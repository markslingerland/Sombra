using Microsoft.AspNetCore.Mvc.Rendering;
using Sombra.Web.Infrastructure.Authentication;

namespace Sombra.Web.Infrastructure
{
    public static class HtmlHelperExtensions
    {
        public static SombraPrincipal GetUser(this IHtmlHelper helper)
        {
            return helper.ViewContext.HttpContext.GetUser();
        }
    }
}