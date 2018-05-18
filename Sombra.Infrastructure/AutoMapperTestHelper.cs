using AutoMapper;

namespace Sombra.Infrastructure
{
    public static class AutoMapperTestHelper
    {
        public static IMapper GetMapper(Profile profile)
        {
            if (MapperInstance != null) return MapperInstance;

            Mapper.Initialize(cfg => cfg.AddProfile(profile));
            var config = Mapper.Configuration;
            config.AssertConfigurationIsValid();
            MapperInstance = new Mapper(config);

            return MapperInstance;
        }

        private static IMapper MapperInstance { get; set; }
    }
}