using AutoMapper;
using Sombra.Core;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.User;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreatedEvent, User>()
                .IgnoreEntityProperties()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => Helpers.GetUserName(s)));
        }
    }
}