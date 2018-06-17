using Microsoft.AspNetCore.Mvc.Rendering;
using Sombra.Web.Infrastructure.Authentication;

namespace Sombra.Web.Infrastructure.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static SombraPrincipal GetUser(this IHtmlHelper helper)
        {
            return helper.ViewContext.HttpContext.GetUser();
        }

        public static string SubdomainUrl(this IHtmlHelper helper, string subdomain)
        {
            return $"{helper.ViewContext.HttpContext.GetScheme()}{subdomain}.{helper.ViewContext.HttpContext.GetHost()}";
        }

        public static string HomeUrl(this IHtmlHelper helper)
        {
            return helper.ViewContext.HttpContext.GetBaseUrl();
        }

        public static string BaseUrl(this IHtmlHelper helper, string url)
        {
            return $"{helper.ViewContext.HttpContext.GetBaseUrl()}{url}";
        }
    }
}