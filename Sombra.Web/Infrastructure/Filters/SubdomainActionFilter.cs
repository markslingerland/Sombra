using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Sombra.Web.Controllers;

namespace Sombra.Web.Infrastructure.Filters
{
    public class SubdomainActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is CharityController)
                if (!context.RouteData.Values.ContainsKey(CharityController.SubdomainParameter))
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "Controller", ControllerExtensions.GetName(typeof(HomeController)) },
                            { "Action", nameof(HomeController.Index) }
                        });
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}