using AutoMapper;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserResponse>()
                .ForMember(d => d.UserExists, opt => opt.MapFrom(s => true));
        }
    }
}