using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Events.Email;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Requests.Template;
using Sombra.Messaging.Requests.User;
using Sombra.Web.Infrastructure;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Services
{
    public class UserService
    {
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;
        private readonly string _baseUrl;
        private readonly string _userAgent;

        public UserService(ICachingBus bus, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _bus = bus;
            _mapper = mapper;
            _baseUrl = httpContextAccessor.HttpContext.Request.Host.ToString();
            _userAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
        }

        public async Task<RegisterAccountResultViewModel> RegisterAccount(RegisterAccountViewModel model)
        {
            var emailExistsMessage = "Dit e-mailadres kan niet worden gebruikt om een account te registeren!";

            var emailExistsRequest = _mapper.Map<UserEmailExistsRequest>(model);
            var emailExistsResponse = await _bus.RequestAsync(emailExistsRequest);
            if (emailExistsResponse.EmailExists)
            {
                return new RegisterAccountResultViewModel
                {
                    Message = emailExistsMessage
                };
            }

            var userKey = Guid.NewGuid();
            var createIdentityRequest = _mapper.Map<CreateIdentityRequest>(model);
            createIdentityRequest.UserKey = userKey;

            var createIdentityResponse = await _bus.RequestAsync(createIdentityRequest);
            if (createIdentityResponse.Success)
            {
                var createUserRequest = _mapper.Map<CreateUserRequest>(model);
                createUserRequest.UserKey = userKey;
                var createUserResponse = await _bus.RequestAsync(createUserRequest);
                if (createUserResponse.Success)
                {
                    await SendActivationTokenEmail(createIdentityRequest.UserName, model.EmailAddress, createIdentityResponse.ActivationToken);

                    return new RegisterAccountResultViewModel
                    {
                        Success = true,
                        Message = $"Het account is succesvol geregistreerd! Er is een e-mail verzonden naar {model.EmailAddress} om het account te activeren."
                    };
                }

                return new RegisterAccountResultViewModel
                {
                    Message = "Er is een fout opgetreden bij het afronden van de registratie van je account. Probeer het later opnieuw!"
                };

            }

            if (createIdentityResponse.ErrorType == ErrorType.EmailExists)
            {
                return new RegisterAccountResultViewModel
                {
                    Message = emailExistsMessage
                };
            }
            return new RegisterAccountResultViewModel
            {
                Message = "Er is een fout opgetreden bij het registeren van je account. Probeer het later opnieuw!"
            };
        }

        public async Task<ActivateAccountResultViewModel> ActivateAccount(ActivateAccountViewModel model)
        {
            var request = _mapper.Map<ActivateUserRequest>(model);
            var response = await _bus.RequestAsync(request);

            if (response.Success)
            {
                return new ActivateAccountResultViewModel
                {
                    Success = true,
                    Message = "Je account is geactiveerd!"
                };
            }


            if (response.ErrorType == ErrorType.TokenInvalid || response.ErrorType == ErrorType.TokenExpired)
            {
                return new ActivateAccountResultViewModel
                {
                    Message = "Je account kon niet worden geactiveerd omdat de gebruikte link ongeldig is. Vraag een nieuwe activatie-email aan en probeer het opnieuw"
                };
            }

            return new ActivateAccountResultViewModel
            {
                Message = "Er is een fout opgetreden bij het activeren van je account. Probeer het later opnieuw!"
            };
        }

        public async Task<RequestActivationTokenResultViewModel> RequestActivationToken(RequestActivationTokenViewModel model)
        {
            var request = _mapper.Map<GetUserActivationTokenRequest>(model);
            var response = await _bus.RequestAsync(request);
            if (response.HasActivationToken)
            {
                await SendActivationTokenEmail(response.UserName, request.EmailAddress, response.ActivationToken);
                return new RequestActivationTokenResultViewModel
                {
                    Success = true,
                    Message = $"Er is een e-mail verzonden naar {request.EmailAddress} om het account te activeren."
                };
            }

            if (response.ErrorType == ErrorType.InvalidEmail)
            {
                return new RequestActivationTokenResultViewModel
                {
                    Message = "Het opgegeven e-mailadres is ongeldig!"
                };
            }

            if (response.ErrorType == ErrorType.AlreadyActive)
            {
                return new RequestActivationTokenResultViewModel
                {
                    Message = "Het account met dit e-mailadres is al geactiveerd!"
                };
            }

            return new RequestActivationTokenResultViewModel
            {
                Message = "Er is een fout opgetreden bij het opnieuw aanvragen van een activatiecode. Probeer het later opnieuw!"
            };
        }

        private async Task SendActivationTokenEmail(string userName, string emailAddress, string activationToken)
        {
            var actionurl = $"{_baseUrl}/Account/ActivateAccount?Token={activationToken}";
            var emailTemplateRequest = new EmailTemplateRequest(EmailType.ConfirmAccount);
            var response = await _bus.RequestAsync(emailTemplateRequest);

            var template = TemplateContentBuilder.Build(response.Template,
                TemplateContentBuilder.CreateConfirmAccountTemplateContent(userName, actionurl));

            var email = new EmailEvent(new EmailAddress("noreply", "noreply@ikdoneer.nu"), new EmailAddress(userName, emailAddress), "Account activatie ikdoneer.nu",
                template, true);

            await _bus.PublishAsync(email);
        }

        public async Task<bool> ChangePassword(ChangePasswordViewModel changePasswordViewModel, string securityToken)
        {
            if (!string.IsNullOrEmpty(securityToken))
            {

                if (changePasswordViewModel.Password == changePasswordViewModel.VerifiedPassword)
                {
                    var changePasswordRequest = new ChangePasswordRequest(Core.Encryption.CreateHash(changePasswordViewModel.Password), securityToken);
                    var response = await _bus.RequestAsync(changePasswordRequest);
                    return response.Success;
                }
            }
            return false;
        }

        public async Task<bool> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            var forgotPasswordRequest = new ForgotPasswordRequest(forgotPasswordViewModel.EmailAddress);
            var getUserByEmailRequest = new GetUserByEmailRequest { EmailAddress = forgotPasswordViewModel.EmailAddress };

            var clientInfo = UserAgentParser.Extract(_userAgent);
            var user = await _bus.RequestAsync(getUserByEmailRequest);
            var name = Helpers.GetUserName(user);
            var forgotPasswordResponse = await _bus.RequestAsync(forgotPasswordRequest);
            var actionurl = $"{_baseUrl}/Account/ChangePassword/{forgotPasswordResponse.Secret}";

            var emailTemplateRequest = new EmailTemplateRequest(EmailType.ForgotPassword);
            var response = await _bus.RequestAsync(emailTemplateRequest);
            var template = TemplateContentBuilder.Build(response.Template,
                TemplateContentBuilder.CreateForgotPasswordTemplateContent(name, actionurl, clientInfo.OperatingSystem, clientInfo.BrowserName));

            var email = new EmailEvent(new EmailAddress("noreply", "noreply@ikdoneer.nu"), new EmailAddress(name, forgotPasswordViewModel.EmailAddress), "Wachtwoord vergeten ikdoneer.nu",
                template, true);

            await _bus.PublishAsync(email);

            return true;
        }
    }
}