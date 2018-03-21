using AutoMapper;
using Sombra.Messaging.Responses;

namespace Sombra.LoggingService
{
    public static class LoggingMapper
    {
        public static IMapper Mapper { get; }

        static LoggingMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LogEntry, Log>();
                });
            
            mapperConfiguration.AssertConfigurationIsValid();
            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}