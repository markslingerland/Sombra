using AutoMapper;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;
using Sombra.Messaging.Shared;
using Charity = Sombra.CharityActionService.DAL.Charity;
using CharityAction = Sombra.CharityActionService.DAL.CharityAction;

namespace Sombra.CharityActionService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCharityActionRequest, CharityAction>()
                .ForMember(d => d.IsApproved, opt => opt.UseValue(false))
                .ForMember(d => d.Charity, opt => opt.Ignore())
                .ForMember(d => d.CharityId, opt => opt.Ignore());

            CreateMap<CharityCreatedEvent, Charity>();

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

            CreateMap<Charity, KeyNamePair>()
                .ForMember(d => d.Key, opt => opt.MapFrom(s => s.CharityKey))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}