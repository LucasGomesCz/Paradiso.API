namespace Paradiso.API.Service.Handlers;

public class AuxiliarHandler : IAuxiliarHandler
{
    private readonly IMapper _mapper;
    private readonly DbSet<Area> _area;
    private readonly DbSet<Genre> _genre;
    private readonly DbSet<KindMovie> _kindMovie;
    private readonly DbSet<State> _state;
    private readonly DbSet<City> _city;

    public AuxiliarHandler(EFContext context, IMapper mapper)
    {
        _area = context.Set<Area>();
        _genre = context.Set<Genre>();
        _kindMovie = context.Set<KindMovie>();
        _state = context.Set<State>();
        _city = context.Set<City>();
        _mapper = mapper;
    }

    public async Task<List<Area>> GetAreaAsync(AreaGetParams @params)
    {
        IQueryable<Area> query = _area.AsNoTracking();

        if(!string.IsNullOrEmpty(@params.Area))
        {
            List<Guid> split = new();

            foreach (var item in @params.Area.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.Id));
        }

        return await query.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<List<City>> GetCityAsync(CityGetParams @params)
    {
        IQueryable<City> query = _city.AsNoTracking();

        if (!string.IsNullOrEmpty(@params.City))
        {
            List<Guid> split = new();

            foreach (var item in @params.City.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.Id));
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

            query = query.Where(x => split.Contains(x.StateId));
        }

        return await query.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<List<Genre>> GetGenreAsync(GenreGetParams @params)
    {
        IQueryable<Genre> query = _genre.AsNoTracking();

        if (!string.IsNullOrEmpty(@params.Genre))
        {
            List<Guid> split = new();

            foreach (var item in @params.Genre.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.Id));
        }

        return await query.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<List<KindMovie>> GetKindMovieAsync(KindMovieGetParams @params)
    {
        IQueryable<KindMovie> query = _kindMovie.AsNoTracking();

        if (!string.IsNullOrEmpty(@params.KindMovie))
        {
            List<Guid> split = new();

            foreach (var item in @params.KindMovie.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.Id));
        }

        return await query.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<List<State>> GetStateAsync(StateGetParams @params)
    {
        IQueryable<State> query = _state.AsNoTracking();

        if (!string.IsNullOrEmpty(@params.State))
        {
            List<Guid> split = new();

            foreach (var item in @params.State.Split(","))
            {
                if (!Guid.TryParse(item, out var id))
                    throw new ExceptionDto() { Message = EException.InvalidValue.DisplayName() };

                split.Add(id);
            }

            query = query.Where(x => split.Contains(x.Id));
        }

        return await query.OrderBy(x => x.Name).ToListAsync();
    }
}
