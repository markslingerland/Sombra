using System.Linq;
using AutoMapper;

namespace Sombra.Infrastructure
{
    public static class AutoMapperHelper
    {
        public static IMapper BuildMapper(Profile profile, params Profile[] profiles)
        {
            if (MapperInstance != null) return MapperInstance;
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(profile);
                if (profiles != null)
                    foreach (var p in profiles)
                        cfg.AddProfile(p);
            });
            var config = Mapper.Configuration;
            config.AssertConfigurationIsValid();
            MapperInstance = new Mapper(config);

            return MapperInstance;
        }

        private static IMapper MapperInstance { get; set; }
    }
}