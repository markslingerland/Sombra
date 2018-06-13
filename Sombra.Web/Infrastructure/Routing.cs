using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Infrastructure
{
    public class Routing
    {
        public static void Setup(IRouteBuilder routeBuilder)
        {
            var hostnames = new[] { "localhost", "ikdoneer.nu" };

            routeBuilder.MapSubdomainRoute(
                hostnames,
                name: "SubdomainRoute",
                subdomain: $"{{{SubdomainViewModel.SUBDOMAIN_PARAMETER}}}",
                template: "{action}",
                defaults: new { controller = "Charity", action = "Index" });

            routeBuilder.MapRoute(
                hostnames,
                name: "goede-doelen",
                template: "goede-doelen",
                defaults: new { controller = "Charity", action = "Search" });

            //routeBuilder.MapRoute(
            //    hostnames,
            //    name: "areas",
            //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            routeBuilder.MapRoute(
                hostnames,
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}