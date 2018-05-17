using AutoMapper;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.CharityService.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events;

namespace Sombra.CharityService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCharityRequest, Charity>()
                .IgnoreEntityProperties();
            CreateMap<Charity, GetCharityByKeyResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
            CreateMap<Charity, CharityCreatedEvent>();
            CreateMap<Charity, CharityUpdatedEvent>();
        }
    }
}