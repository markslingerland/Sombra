using System;
using AutoMapper;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityService.DAL;
using Sombra.Infrastructure.Extensions;

namespace Sombra.CharityService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CharityEntity, CreateCharityResponse>()
                .ForMember(d => d.Success, opt => opt.MapFrom(s => true));
            CreateMap<CreateCharityRequest, CharityEntity>()
                .IgnoreEntityProperties();
            CreateMap<CharityEntity, GetCharityResponse>()
                .ForMember(d => d.Success, opt => opt.MapFrom(s => true));
            CreateMap<GetCharityRequest, CharityEntity>()
                .IgnoreEntityProperties();
            CreateMap<CharityEntity, UpdateCharityResponse>()
                .ForMember(d => d.Success, opt => opt.MapFrom(s => true));
            CreateMap<UpdateCharityRequest, CharityEntity>()
                .IgnoreEntityProperties();
        }
    }
}