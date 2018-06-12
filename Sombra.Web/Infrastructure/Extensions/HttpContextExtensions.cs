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
                _homeUrl = $"{context.Request.Scheme}//{context.Request.Host.Value}";

            return _homeUrl;
        }

        public static SombraPrincipal GetUser(this HttpContext context)
        {
            return context.User != null ? (SombraPrincipal) context.User : default;
        }
    }
}