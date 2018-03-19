using AutoMapper;
using Sombra.Messaging.Responses;

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