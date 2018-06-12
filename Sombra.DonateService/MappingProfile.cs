using System;
using AutoMapper;
using Sombra.Core;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Responses.Donate;
using Sombra.Messaging.Shared;
using Charity = Sombra.DonateService.DAL.Charity;
using CharityAction = Sombra.DonateService.DAL.CharityAction;
using User = Sombra.DonateService.DAL.User;

namespace Sombra.DonateService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreatedEvent, User>()
                .ForMember(d => d.CharityActionDonations, opt => opt.Ignore())
                .ForMember(d => d.CharityDonations, opt => opt.Ignore())
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => Helpers.GetUserName(s)));

            CreateMap<CharityCreatedEvent, Charity>()
                .ForMember(d => d.ChartityActions, opt => opt.Ignore())
                .ForMember(d => d.ChartityDonations, opt => opt.Ignore());

            CreateMap<CharityActionCreatedEvent, CharityAction>()
                .ForMember(d => d.ChartityActionDonations, opt => opt.Ignore())
                .ForMember(d => d.CharityId, opt => opt.Ignore())
                .ForMember(d => d.Charity, opt => opt.Ignore());

            CreateMap<MakeDonationRequest, CharityActionDonation>()
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.UserId, opt => opt.Ignore())
                .ForMember(d => d.CharityAction, opt => opt.Ignore())
                .ForMember(d => d.CharityActionId, opt => opt.Ignore())
                .ForMember(d => d.DateTimeStamp, opt => opt.UseValue(DateTime.UtcNow));

            CreateMap<MakeDonationRequest, CharityDonation>()
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.UserId, opt => opt.Ignore())
                .ForMember(d => d.Charity, opt => opt.Ignore())
                .ForMember(d => d.CharityId, opt => opt.Ignore())
                .ForMember(d => d.DateTimeStamp, opt => opt.UseValue(DateTime.UtcNow));

            CreateMap<CharityDonation, Donation>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserId.HasValue ? s.User.UserName : null))
                .ForMember(d => d.ProfileImage, opt => opt.MapFrom(s => s.UserId.HasValue ? s.User.ProfileImage : null));

            CreateMap<Charity, KeyNamePair>()
                .ForMember(d => d.Key, opt => opt.MapFrom(s => s.CharityKey))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

            CreateMap<CharityAction, KeyNamePair>()
                .ForMember(d => d.Key, opt => opt.MapFrom(s => s.CharityActionKey))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}