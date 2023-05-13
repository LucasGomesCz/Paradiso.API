namespace Paradiso.API.Config;

public static class Services
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<EFContext>(options => options
                                                    .UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")!)
                                                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                                                    .EnableSensitiveDataLogging()
        );

        services.AddScoped<BlobServiceClient>(options => new(builder.Configuration.GetConnectionString("BlobStorage")!));

        services.AddControllers();

        services.AddHealthChecks();

        services.AddSwaggerGen();

        services.AddScoped<EFContext>();

        services.AddScoped<IAuxiliarHandler, AuxiliarHandler>();

        services.AddScoped<IUserHandler, UserHandler>();

        services.AddScoped<IMovieHandler, MovieHandler>();

        services.AddScoped<IScriptHandler, ScriptHandler>();

        services.AddScoped<IPhotoHandler, PhotoHandler>();

        services.AddScoped<ISoundTrackHandler, SoundTrackHandler>();

        return services;
    }
}
