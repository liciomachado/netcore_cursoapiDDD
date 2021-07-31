using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCuting.Mappings
{
    public class ConfigureMappingProfile
    {
        public static void ConfigureDependenciesAutoMapper(IServiceCollection serviceCollection)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }
    }
}
