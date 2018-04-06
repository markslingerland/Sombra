using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sombra.IdentityService;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;


namespace Sombra.Web
{
  public interface IUserManager
  {
    Task<UserLoginResponse> ValidateAsync(UserLoginRequest userLoginRequest);
    Task<bool> SignInAsync(HttpContext httpContext, UserLoginRequest userLoginRequest, bool isPersistent = false);
    void SignOut(HttpContext httpContext);

    int GetCurrentUserId(HttpContext httpContext);
    // User GetCurrentUser(HttpContext httpContext);
  }
}