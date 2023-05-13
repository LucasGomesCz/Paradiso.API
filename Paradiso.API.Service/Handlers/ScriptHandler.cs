namespace Paradiso.API.Service.Handlers;

public class ScriptHandler : IScriptHandler
{
    private readonly IMapper _mapper;
    private readonly BlobContainerClient _blob;
    private readonly EFContext _context;
    private readonly DbSet<Script> _script;
    private readonly DbSet<UserScript> _userScript;

    public ScriptHandler(EFContext context, BlobServiceClient blob, IMapper mapper)
    {
        _context = context;
        _script = context.Set<Script>();
        _userScript = context.Set<UserScript>();
        _blob = blob.GetBlobContainerClient(EContainer.Scripts.ToString());
        _mapper = mapper;
    }

    public async Task<List<Script>> GetAsync(ScriptGetParams @params)
    {
        IQueryable<Script> query = _script.AsNoTracking();

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

        if (@params.IsComplete.HasValue)
        {
            query = query.Where(x => x.IsComplete == @params.IsComplete.Value);
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

    public async Task<MessageDto> UploadAsync(ScriptPostParams @params)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var scriptId = Guid.NewGuid();

            var hash = Blob.GetHashCodeFromFile(@params.File);

            var url = await Blob.UploadBlobFileAsync(_blob, @params.File, scriptId, hash);

            await _script.AddAsync(new Script
            {
                Id = scriptId,
                Name = @params.Name,
                ReleaseDate = DateTime.Today,
                IsComplete = @params.IsComplete,
                HasCopyright = @params.HasCopyright,
                Description = @params.Description ?? null,
                HashCode = hash,
                Extension = Path.GetExtension(@params.File.FileName),
                Url = url,
                GenreId = @params.GenreId,
            });

            await _userScript.AddAsync(new UserScript
            {
                Id = Guid.NewGuid(),
                UserId = @params.UserId,
                ScriptId = scriptId,
                IsOwner = true
            });

            if (@params.Cast is not null)
            {
                var lst = @params.Cast.Distinct().Select(castMember => new UserScript
                {
                    Id = Guid.NewGuid(),
                    UserId = castMember,
                    ScriptId = scriptId,
                    IsOwner = false
                }).ToList();

                await _userScript.AddRangeAsync(lst);
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

    public async Task<MessageDto> UpdateAsync(ScriptPutParams @params)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var obj = await _script.AsNoTracking().FirstOrDefaultAsync(x => x.Id == new Guid());

            if (obj is null)
                throw new ExceptionDto() { Message = EException.ScriptNotFound.DisplayName() };

            if (!string.IsNullOrEmpty(@params.Name))
            {
                obj.Name = @params.Name;
            }

            if (@params.IsComplete.HasValue)
            {
                obj.IsComplete = @params.IsComplete.Value;
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
                var userScripts = await _userScript.AsNoTracking().Where(x => x.ScriptId == @params.Id).ToListAsync();

                var castUserScripts = @params.Cast.Distinct().Select(castMember => new UserScript
                {
                    Id = Guid.NewGuid(),
                    UserId = castMember,
                    ScriptId = obj.Id,
                    IsOwner = false
                }).ToList();

                var scriptsToRemove = userScripts.Except(castUserScripts).ToList();
                var scriptsToAdd = castUserScripts.Except(userScripts).ToList();

                _userScript.RemoveRange(scriptsToRemove);
                await _userScript.AddRangeAsync(scriptsToAdd);
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

        var lst = await _script.AsNoTracking().Where(x => split.Contains(x.Id)).ToListAsync();

        var userScriptsToDelete = await _userScript.AsNoTracking().Where(x => lst.Any(y => x.ScriptId == y.Id)).ToListAsync();

        if (lst is null)
            return new() { Message = EException.ScriptNotFound.DisplayName() };

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _userScript.RemoveRange(userScriptsToDelete);

            _script.RemoveRange(lst);

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
