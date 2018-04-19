using System.Threading.Tasks;
using Sombra.Messaging.Requests;

namespace Sombra.Web.Infrastructure.Authentication
{
    public interface IUserManager
    {
        Task<bool> ValidateAsync(UserLoginRequest userLoginRequest);
        Task<bool> SignInAsync(UserLoginRequest userLoginRequest, bool isPersistent = false);
        Task SignOut();
    }
}