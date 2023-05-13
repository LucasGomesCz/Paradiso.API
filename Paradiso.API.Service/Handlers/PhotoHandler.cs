namespace Paradiso.API.Service.Handlers;

public class PhotoHandler : IPhotoHandler
{
    private readonly IMapper _mapper;
    private readonly BlobContainerClient _blob;
    private readonly EFContext _context;
    private readonly DbSet<Photo> _photo;
    private readonly DbSet<UserPhoto> _userPhoto;

    public PhotoHandler(EFContext context, BlobServiceClient blob, IMapper mapper)
    {
        _context = context;
        _photo = context.Set<Photo>();
        _userPhoto = context.Set<UserPhoto>();
        _blob = blob.GetBlobContainerClient(EContainer.Photos.ToString());
        _mapper = mapper;
    }

    public async Task<List<Photo>> GetAsync(PhotoGetParams @params)
    {
        IQueryable<Photo> query = _photo.AsNoTracking();

        if (!string.IsNullOrEmpty(@params.Id))
        {
            List<Guid> split = new();

            foreach (var item in @params.Id.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.Id));
        }

        if (!string.IsNullOrEmpty(@params.Name))
        {
            var split = @params.Name.Split(",");
            query = query.Where(x => split.Contains(x.Name));
        }

        if (!string.IsNullOrEmpty(@params.MinYear))
        {
            var year = DateTime.ParseExact(@params.MinYear, "yyyy", CultureInfo.InvariantCulture);

            query = string.IsNullOrEmpty(@params.MaxYear)
                ? query.Where(x => x.ReleaseDate == year)
                : query.Where(x => x.ReleaseDate >= year);
        }

        if (!string.IsNullOrEmpty(@params.MaxYear))
        {
            var year = DateTime.ParseExact(@params.MaxYear, "yyyy", CultureInfo.InvariantCulture);
            query = query.Where(x => x.ReleaseDate <= year);
        }

        if (@params.HasCopyright.HasValue)
        {
            query = query.Where(x => x.HasCopyright == @params.HasCopyright.Value);
        }

        if (!string.IsNullOrEmpty(@params.Genre))
        {
            List<Guid> split = new();

            foreach (var item in @params.Genre.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.Genre.Id));
        }

        int skip = @params.Page.HasValue && @params.Rows.HasValue ? (@params.Page.Value - 1) * @params.Rows.Value : 0;
        int take = @params.Rows ?? 10;

        return await query.OrderBy(x => x.Name).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<MessageDto> UploadAsync(PhotoPostParams @params)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var photoId = Guid.NewGuid();

            var hash = Blob.GetHashCodeFromFile(@params.File);

            var url = await Blob.UploadBlobFileAsync(_blob, @params.File, photoId, hash);

            await _photo.AddAsync(new Photo
            {
                Id = photoId,
                Name = @params.Name,
                ReleaseDate = DateTime.Today,
                HasCopyright = @params.HasCopyright,
                Description = @params.Description ?? null,
                HashCode = hash,
                Extension = Path.GetExtension(@params.File.FileName),
                Url = url,
                GenreId = @params.GenreId,
            });

            await _userPhoto.AddAsync(new UserPhoto
            {
                Id = Guid.NewGuid(),
                UserId = @params.UserId,
                PhotoId = photoId,
                IsOwner = true
            });

            if (@params.Cast is not null)
            {
                var lst = @params.Cast.Distinct().Select(castMember => new UserPhoto
                {
                    Id = Guid.NewGuid(),
                    UserId = castMember,
                    PhotoId = photoId,
                    IsOwner = false
                }).ToList();

                await _userPhoto.AddRangeAsync(lst);
            }

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new ExceptionDto() { Message = ex.Message };
        }
    }

    public async Task<MessageDto> UpdateAsync(PhotoPutParams @params)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var obj = await _photo.AsNoTracking().FirstOrDefaultAsync(x => x.Id == new Guid());

            if (obj is null)
                throw new ExceptionDto() { Message = EException.PhotoNotFound.DisplayName() };

            if (!string.IsNullOrEmpty(@params.Name))
            {
                obj.Name = @params.Name;
            }

            if (@params.HasCopyright.HasValue)
            {
                obj.HasCopyright = @params.HasCopyright.Value;
            }

            if (!string.IsNullOrEmpty(@params.Description))
            {
                obj.Description = @params.Description;
            }

            if (@params.GenreId.HasValue)
            {
                obj.GenreId = @params.GenreId.Value;
            }

            if (@params.Cast is not null)
            {
                var userPhotos = await _userPhoto.AsNoTracking().Where(x => x.PhotoId == @params.Id).ToListAsync();

                var castUserPhotos = @params.Cast.Distinct().Select(castMember => new UserPhoto
                {
                    Id = Guid.NewGuid(),
                    UserId = castMember,
                    PhotoId = obj.Id,
                    IsOwner = false
                }).ToList();

                var photosToRemove = userPhotos.Except(castUserPhotos).ToList();
                var photosToAdd = castUserPhotos.Except(userPhotos).ToList();

                _userPhoto.RemoveRange(photosToRemove);
                await _userPhoto.AddRangeAsync(photosToAdd);
            }

            if (@params.File is not null)
            {
                obj.Url = await Blob.UpateBlobFileAsync(_blob, @params.File, obj.Id, obj.HashCode);
            }

            _context.Entry(obj).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new ExceptionDto() { Message = ex.Message };
        }
    }

    public async Task<MessageDto> DeleteAsync(DeleteParams @params)
    {
        List<Guid> split = new();

        foreach (var item in @params.Id.Split(","))
        {
            if (!Guid.TryParse(item, out var id))
                throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

            split.Add(id);
        }

        var lst = await _photo.AsNoTracking().Where(x => split.Contains(x.Id)).ToListAsync();

        var userPhotosToDelete = await _userPhoto.AsNoTracking().Where(x => lst.Any(y => x.PhotoId == y.Id)).ToListAsync();

        if (lst is null)
            return new() { Message = EException.PhotoNotFound.DisplayName() };

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _userPhoto.RemoveRange(userPhotosToDelete);

            _photo.RemoveRange(lst);

            var filesToDelete = lst.Select(x => $"{x.Id}.{x.HashCode}{x.Extension}");

            await Blob.DeleteBlobFileAsync(_blob, filesToDelete);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new ExceptionDto() { Message = ex.Message };
        }
    }
}
