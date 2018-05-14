﻿using System;
using AutoMapper;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserByKeyResponse>()
                .ForMember(d => d.UserExists, opt => opt.UseValue(true));

            CreateMap<User, GetUserByEmailResponse>()
                .ForMember(d => d.UserExists, opt => opt.UseValue(true));

            CreateMap<CreateUserRequest, User>()
                .IgnoreEntityProperties()
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow));

            CreateMap<User, UserCreatedEvent>()
                .ForMember(d => d.UserCreated, opt => opt.MapFrom(s => s.Created));

            CreateMap<UpdateUserRequest, User>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Created, opt => opt.Ignore());

            CreateMap<User, UserUpdatedEvent>()
                .ForMember(d => d.UserCreated, opt => opt.MapFrom(s => s.Created));
        }
    }
}