using AutoMapper;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityActionService.DAL;
using Sombra.Infrastructure.Extensions;

namespace Sombra.CharityActionService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CharityAction, CreateCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
            CreateMap<CreateCharityActionRequest, CharityAction>()
                .IgnoreEntityProperties();
            CreateMap<CharityAction, GetCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
            CreateMap<CharityAction, UpdateCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
            CreateMap<UpdateCharityActionRequest, CharityAction>()
                .IgnoreEntityProperties()
                .ForMember(d => d.IsApproved, opt => opt.UseValue(false));
            CreateMap<CharityAction, DeleteCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
        }
    }
}