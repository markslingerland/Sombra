using System.Collections.Concurrent;
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
        private static readonly ConcurrentDictionary<MethodInfo, SubdomainAttribute> SubdomainAttributes = new ConcurrentDictionary<MethodInfo, SubdomainAttribute>();
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var attribute = SubdomainAttributes.GetOrAdd(controllerActionDescriptor.MethodInfo,
                    methodInfo => methodInfo.GetCustomAttribute<SubdomainAttribute>());
                if (attribute != null && !context.RouteData.Values.ContainsKey(SubdomainViewModel.SUBDOMAIN_PARAMETER))
                    context.Result = new RedirectResult($"{context.HttpContext.GetBaseUrl()}/{attribute.Redirect}", false, false);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}