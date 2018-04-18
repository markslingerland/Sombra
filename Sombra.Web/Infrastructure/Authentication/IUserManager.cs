using System.Threading.Tasks;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;


namespace Sombra.Web.Infrastructure.Authentication
{
  public interface IUserManager
  {
    Task<UserLoginResponse> ValidateAsync(UserLoginRequest userLoginRequest);
    Task<bool> SignInAsync(UserLoginRequest userLoginRequest, bool isPersistent = false);
    Task SignOut();

    //int GetCurrentUserId(HttpContext httpContext);
    // User GetCurrentUser(HttpContext httpContext);
  }
}