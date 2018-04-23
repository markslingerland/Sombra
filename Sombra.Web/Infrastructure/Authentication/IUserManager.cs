using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.ViewModels;

namespace Sombra.Web
{
  public interface IUserManager
  {
    Task<UserLoginResponse> ValidateAsync(UserLoginRequest userLoginRequest);
    Task<bool> SignInAsync(HttpContext httpContext, AuthenticationQuery authenticationQuery, bool isPersistent = false);
    void SignOut(HttpContext httpContext);
    Task<bool> ChangePassword(HttpContext httpContext, ChangePasswordViewModel changePasswordViewModel, string id);
    Task<bool> ForgotPassword(HttpContext httpContext, ForgotPasswordViewModel forgotPasswordViewModel);
    

    int GetCurrentUserId(HttpContext httpContext);  

    // User GetCurrentUser(HttpContext httpContext);
  }
}