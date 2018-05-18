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
            CreateMap<CharityActionEntity, CreateCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
            CreateMap<CreateCharityActionRequest, CharityActionEntity>()
                .IgnoreEntityProperties();
            CreateMap<CharityActionEntity, GetCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
            CreateMap<CharityActionEntity, UpdateCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
            CreateMap<UpdateCharityActionRequest, CharityActionEntity>()
                .IgnoreEntityProperties();
            CreateMap<CharityActionEntity, DeleteCharityActionResponse>()
                .ForMember(d => d.Success, opt => opt.UseValue(true));
        }
    }
}