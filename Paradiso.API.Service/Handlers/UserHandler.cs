namespace Paradiso.API.Service.Handlers;

public class UserHandler : IUserHandler
{
    private readonly IMapper _mapper;
    private readonly BlobServiceClient _blob;
    private readonly EFContext _context;
    private readonly DbSet<User> _user;

    public UserHandler(EFContext context, BlobServiceClient blob, IMapper mapper)
    {
        _context = context;
        _user = context.Set<User>();
        _blob = blob;
        _mapper = mapper;
    }

    public async Task<List<User>> GetAsync(UserGetParams @params)
    {
        IQueryable<User> query = _user.AsNoTracking();

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

        if (@params.Gender.HasValue)
        {
            query = query.Where(x => x.Gender == @params.Gender);
        }

        if (@params.MinAge.HasValue)
        {
            query = query.Where(x => Age.Calculate(x.Birthday) >= @params.MinAge.Value);
        }

        if (@params.MaxAge.HasValue)
        {
            query = query.Where(x => Age.Calculate(x.Birthday) <= @params.MaxAge.Value);
        }

        if (@params.IsCreator.HasValue)
        {
            query = query.Where(x => x.IsCreator == @params.IsCreator.Value);
        }

        if (!string.IsNullOrEmpty(@params.Area))
        {
            List<Guid> split = new();

            foreach (var item in @params.Area.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.Area.Id));
        }

        if (!string.IsNullOrEmpty(@params.City))
        {
            List<Guid> split = new();

            foreach (var item in @params.City.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.City.Id));
        }

        if (!string.IsNullOrEmpty(@params.State))
        {
            List<Guid> split = new();

            foreach (var item in @params.State.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.City.State.Id));
        }

        int skip = @params.Page.HasValue && @params.Rows.HasValue ? (@params.Page.Value - 1) * @params.Rows.Value : 0;
        int take = @params.Rows ?? 10;

        return await query.OrderBy(x => x.Name).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<MessageDto> UploadAsync(UserPostParams @params)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _user.AddAsync(new User
            {
                Id = Guid.NewGuid(),
                Name = @params.Name,
                Gender = @params.Gender,
                Birthday = @params.Birthday,
                Email = @params.Email,
                IsCreator = @params.IsCreator,
                Telephone = @params.Telephone ?? null,
                Description = @params.Description ?? null,
                AreaId = @params.AreaId,
                CityId = @params.CityId
            });

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

    public async Task<MessageDto> UpdateAsync(UserPutParams @params)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var obj = await _user.AsNoTracking().FirstOrDefaultAsync(x => x.Id == new Guid());

            if (obj is null)
                throw new ExceptionDto() { Message = EException.UserNotFound.DisplayName() };

            if (!string.IsNullOrEmpty(@params.Name))
            {
                obj.Name = @params.Name;
            }

            if (@params.Gender.HasValue)
            {
                obj.Gender = @params.Gender.Value;
            }

            if (@params.Birthday.HasValue)
            {
                obj.Birthday = @params.Birthday.Value;
            }

            if (!string.IsNullOrEmpty(@params.Email))
            {
                obj.Email = @params.Email;
            }

            if (@params.IsCreator.HasValue)
            {
                obj.IsCreator = @params.IsCreator.Value;
            }

            if (!string.IsNullOrEmpty(@params.Telephone))
            {
                obj.Telephone = @params.Telephone;
            }

            if (!string.IsNullOrEmpty(@params.Description))
            {
                obj.Description = @params.Description;
            }

            if (@params.AreaId.HasValue)
            {
                obj.AreaId = @params.AreaId.Value;
            }

            if (@params.CityId.HasValue)
            {
                obj.CityId = @params.CityId.Value;
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
        if (!Guid.TryParse(@params.Id, out var id))
            throw new ExceptionDto() { Message = EException.InvalidUniqueValue.DisplayName() };

        var user = await _user.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
            throw new ExceptionDto() { Message = EException.UserNotFound.DisplayName() };

        var userMovie = _context.Set<UserMovie>();
        var userPhoto = _context.Set<UserPhoto>();
        var userScript = _context.Set<UserScript>();
        var userSoundTrack = _context.Set<UserSoundTrack>();

        var movie = _context.Set<Movie>();
        var photo = _context.Set<Photo>();
        var script = _context.Set<Script>();
        var soundTrack = _context.Set<SoundTrack>();

        var userMovies = await userMovie.AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        var userPhotos = await userPhoto.AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        var userScripts = await userScript.AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        var userSoundTracks = await userSoundTrack.AsNoTracking().Where(x => x.UserId == id).ToListAsync();

        var moviesToDelete = await movie.AsNoTracking().Where(x => userMovies.Any(y => y.MovieId == x.Id)).ToListAsync();
        var photosToDelete = await photo.AsNoTracking().Where(x => userPhotos.Any(y => y.PhotoId == x.Id)).ToListAsync();
        var scriptsToDelete = await script.AsNoTracking().Where(x => userScripts.Any(y => y.ScriptId == x.Id)).ToListAsync();
        var soundTracksToDelete = await soundTrack.AsNoTracking().Where(x => userSoundTracks.Any(y => y.SoundTrackId == x.Id)).ToListAsync();

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            userMovie.RemoveRange(userMovies);
            userPhoto.RemoveRange(userPhotos);
            userScript.RemoveRange(userScripts);
            userSoundTrack.RemoveRange(userSoundTracks);

            movie.RemoveRange(moviesToDelete);
            photo.RemoveRange(photosToDelete);
            script.RemoveRange(scriptsToDelete);
            soundTrack.RemoveRange(soundTracksToDelete);

            _user.Remove(user);

            var containerMovie = _blob.GetBlobContainerClient(EContainer.Movies.ToString());
            var containerPhoto = _blob.GetBlobContainerClient(EContainer.Photos.ToString());
            var containerScript = _blob.GetBlobContainerClient(EContainer.Scripts.ToString());
            var containerSound = _blob.GetBlobContainerClient(EContainer.Sounds.ToString());

            var blobMoviesToDelete = moviesToDelete.Select(x => $"{x.Id}.{x.HashCode}{x.Extension}");
            var blobPhotosToDelete = photosToDelete.Select(x => $"{x.Id}.{x.HashCode}{x.Extension}");
            var blobScriptsToDelete = scriptsToDelete.Select(x => $"{x.Id}.{x.HashCode}{x.Extension}");
            var blobSoundTracksToDelete = soundTracksToDelete.Select(x => $"{x.Id}.{x.HashCode}{x.Extension}");

            await Blob.DeleteBlobFileAsync(containerMovie, blobMoviesToDelete);
            await Blob.DeleteBlobFileAsync(containerPhoto, blobPhotosToDelete);
            await Blob.DeleteBlobFileAsync(containerScript, blobScriptsToDelete);
            await Blob.DeleteBlobFileAsync(containerSound, blobSoundTracksToDelete);

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
