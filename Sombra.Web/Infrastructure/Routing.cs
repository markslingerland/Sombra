using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Sombra.Web.Controllers;

namespace Sombra.Web.Infrastructure
{
    public class Routing
    {
        public static void Setup(IRouteBuilder routeBuilder)
        {
            var hostnames = new[] { "localhost", "ikdoneer.nu" };

            routeBuilder.MapSubdomainRoute(
                hostnames,
                "SubdomainRoute",
                $"{{{CharityController.SubdomainParameter}}}",
                "{action}",
                new { controller = "Charity", action = "Index" });

            routeBuilder.MapRoute(
                hostnames,
                name: "goede-doelen",
                template: "goede-doelen",
                defaults: new { controller = "Search", action = "Index" });

            routeBuilder.MapRoute(
                hostnames,
                name: "areas",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            routeBuilder.MapRoute(
                hostnames,
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}