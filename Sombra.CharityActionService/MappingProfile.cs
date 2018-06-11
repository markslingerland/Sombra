using AutoMapper;
using Sombra.CharityActionService.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.Charity;
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
                .ForMember(d => d.IsApproved, opt => opt.UseValue(false))
                .ForMember(d => d.Charity, opt => opt.Ignore())
                .ForMember(d => d.CharityId, opt => opt.Ignore());

            CreateMap<CharityCreatedEvent, Charity>()
                .IgnoreEntityProperties();

            CreateMap<CharityAction, Messaging.Shared.CharityAction>()
                .ForMember(d => d.CharityKey, opt => opt.MapFrom(s => s.Charity.CharityKey));

            CreateMap<CharityAction, GetCharityActionByKeyResponse>()
                .ForMember(d => d.CharityAction, opt => opt.MapFrom(s => s))
                .ForMember(d => d.IsSuccess, opt => opt.UseValue(true));

            CreateMap<CharityAction, GetCharityActionByUrlResponse>()
                .ForMember(d => d.CharityAction, opt => opt.MapFrom(s => s))
                .ForMember(d => d.IsSuccess, opt => opt.UseValue(true));

            CreateMap<CharityAction, CharityActionUpdatedEvent>()
                .ForMember(d => d.CharityKey, opt => opt.MapFrom(s => s.Charity.CharityKey));

            CreateMap<CharityAction, CharityActionCreatedEvent>()
                .ForMember(d => d.CharityKey, opt => opt.MapFrom(s => s.Charity.CharityKey));
        }
    }
}