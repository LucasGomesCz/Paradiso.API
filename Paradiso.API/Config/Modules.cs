using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Paradiso.API.Config;

public static class Modules
{
    public static IHostBuilder ConfigureModule(this IHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(x => x.RegisterModule(new ModuleIoc()));

        return builder;
    }
}
