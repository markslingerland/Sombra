using AutoMapper;
using Sombra.CharityService.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCharityRequest, Charity>()
                .ForMember(d => d.IsApproved, opt => opt.UseValue(false));

            CreateMap<Charity, Messaging.Shared.Charity>();
            CreateMap<Charity, GetCharityByKeyResponse>()
                .ForMember(d => d.Charity, opt => opt.MapFrom(s => s))
                .ForMember(d => d.IsSuccess, opt => opt.UseValue(true));

            CreateMap<Charity, GetCharityByUrlResponse>()
                .ForMember(d => d.Charity, opt => opt.MapFrom(s => s))
                .ForMember(d => d.IsSuccess, opt => opt.UseValue(true));

            CreateMap<Charity, CharityCreatedEvent>();
            CreateMap<Charity, CharityUpdatedEvent>();
        }
    }
}