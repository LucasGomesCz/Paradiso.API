using Paradiso.API.Config;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureModule();

builder.Services.ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureApiDocumentation();

var app = builder.Build();

app.ConfigureApiDocumentarionUi();

app.MapControllers();

app.ConfigureMiddlewares();

app.MapHealthChecks(builder.Configuration.GetSection("EndPointsConfig")["APIHealthCheckUrl"]!);

app.Run();
