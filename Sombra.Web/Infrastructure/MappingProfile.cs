using AutoMapper;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Web.Areas.Development.Models;

namespace Sombra.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LogsQuery, LogRequest>();
            CreateMap<Log, LogViewModel>();
        }
    }
}