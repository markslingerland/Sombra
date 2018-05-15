using AutoMapper;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityActionService.DAL;
using Sombra.Infrastructure.Extensions;
using System;

namespace Sombra.CharityActionService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CharityActionEntity, CreateCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.MapFrom(s => true));
            CreateMap<CreateCharityActionRequest, CharityActionEntity>()
                .IgnoreEntityProperties();
            CreateMap<CharityActionEntity, GetCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.MapFrom(s => true));
            CreateMap<GetCharityActionRequest, CharityActionEntity>()
                .IgnoreEntityProperties();
            CreateMap<CharityActionEntity, UpdateCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.MapFrom(s => true));
            CreateMap<UpdateCharityActionRequest, CharityActionEntity>()
                .IgnoreEntityProperties();
        }
    }
}