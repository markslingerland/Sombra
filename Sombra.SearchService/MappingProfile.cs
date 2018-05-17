using AutoMapper;
using Sombra.Messaging.Responses;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Content, SearchResult>();
        }
    }
}