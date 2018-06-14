using Microsoft.AspNetCore.Http;
using Sombra.Web.Infrastructure.Authentication;

namespace Sombra.Web.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        private static string _homeUrl;
        private static string _scheme;
        private static string _host;

        public static string GetHomeUrl(this HttpContext context)
        {
            if (string.IsNullOrEmpty(_homeUrl))
                _homeUrl = $"{context.GetScheme()}{context.GetHost()}";

            return _homeUrl;
        }

        public static string GetHost(this HttpContext context)
        {
            if (string.IsNullOrEmpty(_host))
            {
                var urlParts = context.Request.Host.Value.Split('.');
                _host = urlParts.Length > 1 ? $"{urlParts[urlParts.Length - 2]}.{urlParts[urlParts.Length - 1]}" : urlParts[0];
            }

            return _host;
        }

        public static string GetScheme(this HttpContext context)
        {
            if (string.IsNullOrEmpty(_scheme))
                _scheme = $"{context.Request.Scheme}://";

            return _scheme;
        }

        public static SombraPrincipal GetUser(this HttpContext context)
        {
            return (SombraPrincipal) context.User;
        }
    }
}