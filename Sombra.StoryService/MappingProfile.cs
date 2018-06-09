using System;
using System.Linq;
using AutoMapper;
using Sombra.Core;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
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

            CreateMap<Story, Messaging.Shared.Story>()
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images.Select(i => i.Base64)));

            CreateMap<Story, GetStoryByKeyResponse>()
                .ForMember(d => d.IsSuccess, opt => opt.UseValue(true))
                .ForMember(d => d.Story, opt => opt.MapFrom(s => s));

            CreateMap<CreateStoryRequest, Story>()
                .IgnoreEntityProperties()
                .ForMember(d => d.Author, opt => opt.Ignore())
                .ForMember(d => d.AuthorId, opt => opt.Ignore())
                .ForMember(d => d.IsApproved, opt => opt.UseValue(false))
                .ForMember(d => d.Created, opt => opt.UseValue(DateTime.UtcNow))
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images.Select(i => new Image {Base64 = i})));
        }
    }
}