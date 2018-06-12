using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Sombra.Web.Infrastructure.Extensions;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Infrastructure.Filters
{
    public class SubdomainActionFilter : IActionFilter
    {
        private static readonly ConcurrentDictionary<MethodInfo, bool> HasSubdomainAttribute = new ConcurrentDictionary<MethodInfo, bool>();
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var requiresSubdomain = HasSubdomainAttribute.GetOrAdd(controllerActionDescriptor.MethodInfo,
                    methodInfo => methodInfo.GetCustomAttributes(typeof(SubdomainAttribute)).Any());
                if (requiresSubdomain && !context.RouteData.Values.ContainsKey(SubdomainViewModel.SUBDOMAIN_PARAMETER))
                    context.Result = new RedirectResult(context.HttpContext.GetHomeUrl(), false, false);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}