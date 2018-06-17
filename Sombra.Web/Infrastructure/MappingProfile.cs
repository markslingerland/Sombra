using System;
using AutoMapper;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Requests.Logging;
using Sombra.Messaging.Requests.Search;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.Donate;
using Sombra.Messaging.Responses.Identity;
using Sombra.Messaging.Responses.Logging;
using Sombra.Messaging.Responses.Story;
using Sombra.Messaging.Shared;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.ViewModels;
using Sombra.Web.ViewModels.Charity;
using Sombra.Web.ViewModels.Donate;
using Sombra.Web.ViewModels.Home;
using Sombra.Web.ViewModels.Shared;
using Sombra.Web.ViewModels.Story;
using GetCharitiesRequest = Sombra.Messaging.Requests.Charity.GetCharitiesRequest;
using GetCharityActionsRequest = Sombra.Messaging.Requests.CharityAction.GetCharityActionsRequest;
using GetCharityActionsResponse = Sombra.Messaging.Responses.CharityAction.GetCharityActionsResponse;
using SearchQuery = Sombra.Web.ViewModels.Charity.SearchQuery;
using SearchResultsViewModel = Sombra.Web.ViewModels.Charity.SearchResultsViewModel;
using SearchResultViewModel = Sombra.Web.ViewModels.Charity.SearchResultViewModel;

namespace Sombra.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LogsQuery, LogRequest>();
            CreateMap<Log, LogViewModel>();
            CreateMap<LoginViewModel, UserLoginRequest>()
                .ForMember(d => d.LoginTypeCode, opt => opt.MapFrom(s => s.CredentialType));

            CreateMap<UserLoginResponse, LoginResultViewModel>()
                .ForMember(d => d.Success, opt => opt.MapFrom(s => s.IsSuccess));

            CreateMap<ForgotPasswordViewModel, ForgotPasswordRequest>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.EmailAddress));

            CreateMap<ForgotPasswordViewModel, GetUserByEmailRequest>();

            CreateMap<RegisterAccountViewModel, CreateIdentityRequest>()
                .ForMember(d => d.UserKey, opt => opt.Ignore())
                .ForMember(d => d.Identifier, opt => opt.MapFrom(s => s.EmailAddress))
                .ForMember(d => d.Secret, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => Helpers.GetUserName(s)))
                .ForMember(d => d.CredentialType, opt => opt.UseValue(CredentialType.Email))
                .ForMember(d => d.Role, opt => opt.UseValue(Role.Default));

            CreateMap<RegisterAccountViewModel, CreateUserRequest>()
                .ForMember(d => d.UserKey, opt => opt.Ignore())
                .ForMember(d => d.AddressLine1, opt => opt.Ignore())
                .ForMember(d => d.AddressLine2, opt => opt.Ignore())
                .ForMember(d => d.ZipCode, opt => opt.Ignore())
                .ForMember(d => d.City, opt => opt.Ignore())
                .ForMember(d => d.Country, opt => opt.Ignore())
                .ForMember(d => d.PhoneNumber, opt => opt.Ignore())
                .ForMember(d => d.BirthDate, opt => opt.Ignore())
                .ForMember(d => d.ProfileImage, opt => opt.Ignore());

            CreateMap<RegisterAccountViewModel, UserEmailExistsRequest>()
                .ForMember(d => d.CurrentUserKey, opt => opt.Ignore());

            CreateMap<ActivateAccountViewModel, ActivateUserRequest>()
                .ForMember(d => d.ActivationToken, opt => opt.MapFrom(s => s.Token));

            CreateMap<RequestActivationTokenViewModel, GetUserActivationTokenRequest>();

            CreateMap<TopCharitiesQuery, GetRandomCharitiesRequest>();
            CreateMap<SearchResult, CharityItemViewModel>()
                .ForMember(d => d.Key, opt => opt.MapFrom(s => s.CharityKey))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CharityName));

            CreateMap<Sombra.Messaging.Shared.CharityAction, CharityActionItemViewModel>();
            CreateMap<GetCharityActionsResponse, CharityActionsViewModel>()
                .ForMember(d => d.CharityActions, opt => opt.MapFrom(s => s.Results));

            CreateMap<CharityActionsQuery, GetCharityActionsRequest>()
                .ForMember(d => d.CharityKey, opt => opt.Ignore())
                .ForMember(d => d.OnlyActive, opt => opt.UseValue(true))
                .ForMember(d => d.OnlyApproved, opt => opt.UseValue(true))
                .ForMember(d => d.CharityUrl, opt => opt.Ignore())
                .ForMember(d => d.Keywords, opt => opt.Ignore())
                .ForMember(d => d.SortOrder, opt => opt.UseValue(SortOrder.Asc))
                .ForMember(d => d.Category, opt => opt.Ignore())
                .ForMember(d => d.OnlyUnapproved, opt => opt.Ignore());


            CreateMap<CharityActionsByCharityQuery, GetCharityActionsRequest>()
                .ForMember(d => d.CharityKey, opt => opt.MapFrom(s => Guid.Parse(s.CharityKey)))
                .ForMember(d => d.OnlyActive, opt => opt.UseValue(true))
                .ForMember(d => d.OnlyApproved, opt => opt.UseValue(true))
                .ForMember(d => d.CharityUrl, opt => opt.Ignore())
                .ForMember(d => d.Keywords, opt => opt.Ignore())
                .ForMember(d => d.SortOrder, opt => opt.UseValue(SortOrder.Asc))
                .ForMember(d => d.Category, opt => opt.Ignore())
                .ForMember(d => d.OnlyUnapproved, opt => opt.Ignore());

            CreateMap<CharityQuery, GetCharityByUrlRequest>()
                .ForMember(d => d.Url, opt => opt.MapFrom(s => s.Subdomain));

            CreateMap<Sombra.Messaging.Shared.Charity, CharityViewModel>();

            CreateMap<DonationsInWeekByCharityQuery, GetCharityTotalRequest>()
                .ForMember(d => d.NumberOfDonations, opt => opt.UseValue(3))
                .ForMember(d => d.IncludeCharityActions, opt => opt.UseValue(false))
                .ForMember(d => d.SortOrder, opt => opt.UseValue(SortOrder.Desc))
                .ForMember(d => d.From, opt => opt.UseValue(DateTime.UtcNow.Subtract(TimeSpan.FromDays(7))))
                .ForMember(d => d.To, opt => opt.UseValue(DateTime.UtcNow))
                .ForMember(d => d.CharityKey, opt => opt.MapFrom(s => Guid.Parse(s.CharityKey)));

            CreateMap<GetCharityTotalResponse, DonationsViewModel>();
            CreateMap<DonationItemViewModel, Donation>();

            CreateMap<CharityStoryQuery, GetStoriesRequest>()
                .ForMember(d => d.CharityKey, opt => opt.MapFrom(s => Guid.Parse(s.CharityKey)))
                .ForMember(d => d.AuthorUserKey, opt => opt.Ignore())
                .ForMember(d => d.CharityUrl, opt => opt.Ignore())
                .ForMember(d => d.SortOrder, opt => opt.UseValue(SortOrder.Asc))
                .ForMember(d => d.OnlyApproved, opt => opt.UseValue(true))
                .ForMember(d => d.OnlyUnapproved, opt => opt.Ignore())
                .ForMember(d => d.PageSize, opt => opt.UseValue(1))
                .ForMember(d => d.PageNumber, opt => opt.Ignore());

            CreateMap<Story, ViewModels.Shared.StoryViewModel>();

            CreateMap<SearchQuery, GetCharitiesRequest>()
                .ForMember(d => d.OnlyApproved, opt => opt.UseValue(true))
                .ForMember(d => d.OnlyUnapproved, opt => opt.Ignore())
                .ForMember(d => d.SortOrder, opt => opt.UseValue(SortOrder.Asc));

            CreateMap<Sombra.Messaging.Responses.Charity.GetCharitiesResponse, SearchResultsViewModel>()
                .ForMember(d => d.PageNumber, opt => opt.Ignore())
                .ForMember(d => d.PageSize, opt => opt.Ignore());


            CreateMap<Sombra.Messaging.Shared.Charity, SearchResultViewModel>();
            CreateMap<Sombra.Messaging.Responses.Donate.Charity, ViewModels.Donate.Charity>();
            CreateMap<KeyNamePair, ViewModels.Donate.CharityAction>();

            CreateMap<DonateViewModel, MakeDonationRequest>()
                .ForMember(d => d.UserKey, opt => opt.Ignore());
            CreateMap<MakeDonationResponse, DonateResultViewModel>();

            CreateMap<ViewModels.Story.SearchQuery, GetStoriesRequest>()
                .ForMember(d => d.AuthorUserKey, opt => opt.Ignore())
                .ForMember(d => d.CharityKey, opt => opt.Ignore())
                .ForMember(d => d.CharityUrl, opt => opt.MapFrom(s => s.Subdomain))
                .ForMember(d => d.SortOrder, opt => opt.UseValue(SortOrder.Asc))
                .ForMember(d => d.OnlyApproved, opt => opt.UseValue(true))
                .ForMember(d => d.OnlyUnapproved, opt => opt.Ignore());

            CreateMap<Story, ViewModels.Story.SearchResultViewModel>();
            CreateMap<Story, RandomStoryViewModel>();

            CreateMap<GetStoriesResponse, ViewModels.Story.SearchResultsViewModel>()
                .ForMember(d => d.PageNumber, opt => opt.Ignore())
                .ForMember(d => d.PageSize, opt => opt.Ignore());

            CreateMap<StoryQuery, GetStoryByUrlRequest>()
                .ForMember(d => d.CharityUrl, opt => opt.MapFrom(s => s.Subdomain))
                .ForMember(d => d.StoryUrlComponent, opt => opt.MapFrom(s => s.Url));

            CreateMap<Story, ViewModels.Story.StoryViewModel>();
            CreateMap<GetRandomStoriesResponse, RelatedStoriesViewModel>()
                .ForMember(d => d.Stories, opt => opt.MapFrom(s => s.Results));
            CreateMap<Story, RelatedStoryViewModel>();
        }
    }
}