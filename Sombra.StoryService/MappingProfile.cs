using System;
using System.Linq;
using AutoMapper;
using Sombra.Core;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.Messaging.Shared;
using Sombra.StoryService.DAL;
using Charity = Sombra.StoryService.DAL.Charity;
using Story = Sombra.StoryService.DAL.Story;
using User = Sombra.StoryService.DAL.User;

namespace Sombra.StoryService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreatedEvent, User>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => Helpers.GetUserName(s)));

            CreateMap<Story, Messaging.Shared.Story>()
                .ForMember(d => d.CharityUrl, opt => opt.MapFrom(s =>s.Charity.Url))
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images.Select(i => i.Base64)))
                .ForMember(d => d.CharityKey, opt => opt.MapFrom(s => s.Charity.CharityKey));

            CreateMap<Story, GetStoryByKeyResponse>()
                .ForMember(d => d.IsSuccess, opt => opt.UseValue(true))
                .ForMember(d => d.Story, opt => opt.MapFrom(s => s));

            CreateMap<Story, GetStoryByUrlResponse>()
                .ForMember(d => d.IsSuccess, opt => opt.UseValue(true))
                .ForMember(d => d.Story, opt => opt.MapFrom(s => s));

            CreateMap<CreateStoryRequest, Story>()
                .ForMember(d => d.Author, opt => opt.Ignore())
                .ForMember(d => d.AuthorId, opt => opt.Ignore())
                .ForMember(d => d.Charity, opt => opt.Ignore())
                .ForMember(d => d.CharityId, opt => opt.Ignore())
                .ForMember(d => d.IsApproved, opt => opt.UseValue(false))
                .ForMember(d => d.Created, opt => opt.UseValue(DateTime.UtcNow))
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images.Select(i => new Image {Base64 = i})));

            CreateMap<CharityCreatedEvent, Charity>();

            CreateMap<Charity, KeyNamePair>()
                .ForMember(d => d.Key, opt => opt.MapFrom(s => s.CharityKey))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}