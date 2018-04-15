using AutoMapper;

namespace Sombra.UserService.UnitTests
{
    public static class Helper
    {
        public static IMapper GetMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new MappingProfile()));
            var config = Mapper.Configuration;
            config.AssertConfigurationIsValid();

            return new Mapper(config);
        }
    }
}