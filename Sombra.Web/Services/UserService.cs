using System;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Http;
using Sombra.Core.Enums;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Web.Infrastructure;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Services
{
    public class UserService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly string _baseUrl;
        private readonly string _userAgent;

        public UserService(IBus bus, IMapper mapper, IHttpContextAccessor httpContextAccessor)
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
                    await SendActivationCodeEmail(createIdentityRequest.UserName, model.EmailAddress, createIdentityResponse.ActivationToken);

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


            if (response.ErrorType == ErrorType.TokenInvalid)
            {
                return new ActivateAccountResultViewModel
                {
                    Message = ""
                };
            }

            if (response.ErrorType == ErrorType.TokenExpired)
            {
                return new ActivateAccountResultViewModel
                {
                    Message = ""
                };
            }

            return new ActivateAccountResultViewModel
            {
                Message = ""
            };
        }

        private async Task SendActivationCodeEmail(string userName, string emailAddress, string activationToken)
        {
            var actionurl = $"{_baseUrl}/Account/ActivateAccount?Token={activationToken}";
            var emailTemplateRequest = new EmailTemplateRequest(EmailType.ConfirmAccount, TemplateContentBuilder.CreateConfirmAccountTemplateContent(userName, actionurl));
            var response = await _bus.RequestAsync(emailTemplateRequest);

            var email = new EmailEvent(new EmailAddress("noreply", "noreply@ikdoneer.nu"), new EmailAddress(userName, emailAddress), "Account activatie ikdoneer.nu",
                response.Template, true);

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
            var name = $"{user.FirstName} {user.LastName}";
            var forgotPasswordResponse = await _bus.RequestAsync(forgotPasswordRequest);
            var actionurl = $"{_baseUrl}/Account/ChangePassword/{forgotPasswordResponse.Secret}";

            var emailTemplateRequest = new EmailTemplateRequest(EmailType.ForgotPassword, TemplateContentBuilder.CreateForgotPasswordTemplateContent(name, actionurl, clientInfo.OperatingSystem, clientInfo.BrowserName));
            var response = await _bus.RequestAsync(emailTemplateRequest);

            var email = new EmailEvent(new EmailAddress("noreply", "noreply@ikdoneer.nu"), new EmailAddress(name, forgotPasswordViewModel.EmailAddress), "Wachtwoord vergeten ikdoneer.nu",
                response.Template, true);

            await _bus.PublishAsync(email);

            return true;
        }
    }
}