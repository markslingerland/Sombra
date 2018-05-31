using AutoMapper;
using Sombra.Messaging.Responses.Logging;

namespace Sombra.LoggingService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LogEntry, Log>();
        }
    }
}