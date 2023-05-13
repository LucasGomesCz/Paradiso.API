using Autofac;
using AutoMapper;

namespace Paradiso.API.Config;

public class ModuleIoc : Module
{ 
    protected override void Load(ContainerBuilder builder)
    {
        ConfigIoc.Load(builder);
    }
}

public class ConfigIoc
{
    public static void Load(ContainerBuilder builder)
    {
        builder.Register(context => new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        })).AsSelf().SingleInstance();

        builder.Register(c =>
        {
            var context = c.Resolve<IComponentContext>();
            var config = context.Resolve<MapperConfiguration>();
            return config.CreateMapper(context.Resolve);
        })
         .As<IMapper>()
         .InstancePerLifetimeScope();
    }
}