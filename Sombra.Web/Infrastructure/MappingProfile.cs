﻿using System;
using AutoMapper;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Requests.Logging;
using Sombra.Messaging.Requests.Search;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.CharityAction;
using Sombra.Messaging.Responses.Identity;
using Sombra.Messaging.Responses.Logging;
using Sombra.Messaging.Shared;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.ViewModels;
using Sombra.Web.ViewModels.Charity;
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
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => Helpers.GetUserName(s)))
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
            CreateMap<GetCharityActionsResponse, CharityActionsViewModel>()
                .ForMember(d => d.CharityActions, opt => opt.MapFrom(s => s.Results));

            CreateMap<CharityActionsQuery, GetCharityActionsRequest>()
                .ForMember(d => d.CharityKey, opt => opt.Ignore())
                .ForMember(d => d.OnlyActive, opt => opt.UseValue(true));

            CreateMap<CharityActionsByCharityQuery, GetCharityActionsRequest>()
                .ForMember(d => d.CharityKey, opt => opt.MapFrom(s => Guid.Parse(s.CharityKey)))
                .ForMember(d => d.OnlyActive, opt => opt.UseValue(true));

            CreateMap<CharityQuery, GetCharityByUrlRequest>()
                .ForMember(d => d.Url, opt => opt.MapFrom(s => s.Subdomain));

            CreateMap<Charity, CharityViewModel>();
        }
    }
}