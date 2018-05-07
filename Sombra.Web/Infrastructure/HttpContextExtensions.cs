using Microsoft.AspNetCore.Http;
using Sombra.Web.Infrastructure.Authentication;

namespace Sombra.Web.Infrastructure
{
    public static class HttpContextExtensions
    {
        public static SombraPrincipal GetUser(this HttpContext context)
        {
            return context.User != null ? (SombraPrincipal) context.User : default;
        }
    }
}