using System;
using AutoMapper;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
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
                .IgnoreResponseProperties()
                .ForMember(d => d.UserExists, opt => opt.UseValue(true));

            CreateMap<User, GetUserByEmailResponse>()
                .IgnoreResponseProperties()
                .ForMember(d => d.UserExists, opt => opt.UseValue(true));

            CreateMap<CreateUserRequest, User>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Created, opt => opt.UseValue(DateTime.UtcNow));

            CreateMap<User, UserCreatedEvent>()
                .IgnoreMessageProperties()
                .ForMember(d => d.UserCreated, opt => opt.MapFrom(s => s.Created));

            CreateMap<UpdateUserRequest, User>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Created, opt => opt.Ignore());

            CreateMap<User, UserUpdatedEvent>()
                .IgnoreMessageProperties()
                .ForMember(d => d.UserCreated, opt => opt.MapFrom(s => s.Created));
        }
    }
}