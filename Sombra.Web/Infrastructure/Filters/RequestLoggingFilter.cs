using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Sombra.Messaging;
using Sombra.Messaging.Shared;

namespace Sombra.Web.Infrastructure.Filters
{
    public class RequestLoggingFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Logger.LogWebrequest(new WebRequest
            {
                DateTimeStamp = DateTime.UtcNow,
                Url = context.HttpContext.Request.Path,
                RouteValues = context.RouteData.Values.ToDictionary(pair => pair.Key, pair => pair.Value.ToString())
            });
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}