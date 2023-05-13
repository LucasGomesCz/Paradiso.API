using Microsoft.OpenApi.Models;

namespace Paradiso.API.Config;

public static class ApiDocumentation
{
    public static IServiceCollection ConfigureApiDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Paradiso.API", Version = "v1" });

            Directory.GetFiles("Config/Comments/", "*.xml", SearchOption.TopDirectoryOnly).ToList()
                .ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
        });

        return services;
    }

    public static IApplicationBuilder ConfigureApiDocumentarionUi(this IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paradiso.API v1"));

        return app;
    }
}
