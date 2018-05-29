using AutoMapper;
using Sombra.Messaging.Shared;
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