using System;
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
                var builder = new UriBuilder
                {
                    Scheme = context.Request.Scheme,
                    Host = context.Request.Host.Value
                };
                _homeUrl = builder.Uri.ToString();
            }

            return _homeUrl;
        }

        public static SombraPrincipal GetUser(this HttpContext context)
        {
            return context.User != null ? (SombraPrincipal) context.User : default;
        }
    }
}