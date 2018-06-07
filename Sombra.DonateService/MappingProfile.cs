using System;
using AutoMapper;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.DonateService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreatedEvent, User>()
                .IgnoreEntityProperties()
                .ForMember(d => d.CharityActionDonations, opt => opt.Ignore())
                .ForMember(d => d.CharityDonations, opt => opt.Ignore())
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"));

            CreateMap<MakeDonationRequest, CharityActionDonation>()
                .IgnoreEntityProperties()
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.UserId, opt => opt.Ignore())
                .ForMember(d => d.CharityAction, opt => opt.Ignore())
                .ForMember(d => d.CharityActionId, opt => opt.Ignore())
                .ForMember(d => d.DateTimeStamp, opt => opt.UseValue(DateTime.UtcNow));

            CreateMap<MakeDonationRequest, CharityDonation>()
                .IgnoreEntityProperties()
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.UserId, opt => opt.Ignore())
                .ForMember(d => d.Charity, opt => opt.Ignore())
                .ForMember(d => d.CharityId, opt => opt.Ignore())
                .ForMember(d => d.DateTimeStamp, opt => opt.UseValue(DateTime.UtcNow));       

            CreateMap<CharityDonation, Donation>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserId.HasValue ? s.User.UserName : null))
                .ForMember(d => d.ProfileImage, opt => opt.MapFrom(s => s.UserId.HasValue ? s.User.ProfileImage : null));
        }
    }
}