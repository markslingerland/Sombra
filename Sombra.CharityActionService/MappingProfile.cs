﻿using AutoMapper;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityActionService.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events;

namespace Sombra.CharityActionService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCharityActionRequest, CharityAction>()
                .IgnoreEntityProperties()
                .ForMember(d => d.IsApproved, opt => opt.UseValue(false));
            CreateMap<CharityAction, GetCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
            CreateMap<CharityAction, CharityActionUpdatedEvent>();
            CreateMap<CharityAction, CharityActionCreatedEvent>();
        }
    }
}