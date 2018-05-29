using AutoMapper;
using Sombra.Core.Enums;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Requests.Logging;
using Sombra.Messaging.Requests.Search;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.Identity;
using Sombra.Messaging.Responses.Logging;
using Sombra.Messaging.Shared;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.ViewModels;
using Sombra.Web.ViewModels.Home;

namespace Sombra.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LogsQuery, LogRequest>();
            CreateMap<Log, LogViewModel>();
            CreateMap<LoginViewModel, UserLoginRequest>();
            CreateMap<UserLoginResponse, LoginViewModel>();
            CreateMap<ForgotPasswordViewModel, ForgotPasswordRequest>();
            CreateMap<ForgotPasswordViewModel, GetUserByEmailRequest>();

            CreateMap<RegisterAccountViewModel, CreateIdentityRequest>()
                .ForMember(d => d.UserKey, opt => opt.Ignore())
                .ForMember(d => d.Identifier, opt => opt.MapFrom(s => s.EmailAddress))
                .ForMember(d => d.Secret, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.CredentialType, opt => opt.UseValue(CredentialType.Email));

            CreateMap<RegisterAccountViewModel, CreateUserRequest>()
                .ForMember(d => d.UserKey, opt => opt.Ignore())
                .ForMember(d => d.AddressLine1, opt => opt.Ignore())
                .ForMember(d => d.AddressLine2, opt => opt.Ignore())
                .ForMember(d => d.ZipCode, opt => opt.Ignore())
                .ForMember(d => d.City, opt => opt.Ignore())
                .ForMember(d => d.Country, opt => opt.Ignore())
                .ForMember(d => d.PhoneNumber, opt => opt.Ignore())
                .ForMember(d => d.BirthDate, opt => opt.Ignore());

            CreateMap<RegisterAccountViewModel, UserEmailExistsRequest>()
                .ForMember(d => d.CurrentUserKey, opt => opt.Ignore());

            CreateMap<ActivateAccountViewModel, ActivateUserRequest>()
                .ForMember(d => d.ActivationToken, opt => opt.MapFrom(s => s.Token));

            CreateMap<RequestActivationTokenViewModel, GetUserActivationTokenRequest>();

            CreateMap<TopCharitiesQuery, GetRandomCharitiesRequest>();
            CreateMap<SearchResult, CharityItemViewModel>()
                .ForMember(d => d.Key, opt => opt.MapFrom(s => s.CharityKey))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CharityName));

            CreateMap<CharityActionItemViewModel, CharityAction>();
        }
    }
}