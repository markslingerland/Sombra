namespace Sombra.Web.Infrastructure.Authentication
{
    public class AuthorizeDonator : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                // The user is not authenticated
                return false;
            }

            var user = httpContext.User;
            if (user.IsInRole("Donator"))
            {
                // Donator => let him in
                return true;
            } else {
                return false;
            }
        }
    }
}