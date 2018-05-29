using AutoMapper;
using Sombra.CharityActionService.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

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