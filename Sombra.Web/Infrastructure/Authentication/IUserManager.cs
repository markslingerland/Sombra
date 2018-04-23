using System.Threading.Tasks;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Infrastructure.Authentication
{
    public interface IUserManager
    {
        Task<UserLoginResponse> ValidateAsync(UserLoginRequest userLoginRequest);
        Task<bool> SignInAsync(AuthenticationQuery authenticationQuery, bool isPersistent = false);
        Task SignOut();
        Task<bool> ChangePassword(ChangePasswordViewModel changePasswordViewModel, string securityToken);
        Task<bool> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel);
    }
}