using System;
using AutoMapper;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.User;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, Messaging.Shared.User>();
            CreateMap<User, GetUserByKeyResponse>()
                .ForMember(d => d.UserExists, opt => opt.UseValue(true))
                .ForMember(d => d.User, opt => opt.MapFrom(s => s));

            CreateMap<User, GetUserByEmailResponse>()
                .ForMember(d => d.UserExists, opt => opt.UseValue(true))
                .ForMember(d => d.User, opt => opt.MapFrom(s => s));

            CreateMap<CreateUserRequest, User>()
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