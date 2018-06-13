using Microsoft.AspNetCore.Http;
using Sombra.Web.Infrastructure.Authentication;

namespace Sombra.Web.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        private static string _homeUrl;
        public static string GetHomeUrl(this HttpContext context)
        {
            if (string.IsNullOrEmpty(_homeUrl))
            {
                var urlParts = context.Request.Host.Value.Split('.');
                var host = urlParts.Length > 1 ? $"{urlParts[urlParts.Length - 2]}.{urlParts[urlParts.Length - 1]}" : urlParts[0];
                _homeUrl = $"{context.Request.Scheme}//{host}";
            }

            return _homeUrl;
        }

        public static SombraPrincipal GetUser(this HttpContext context)
        {
            return (SombraPrincipal) context.User;
        }
    }
}