using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sombra.Web.Infrastructure.Filters
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                if (context.HttpContext.Request.Method == "GET")
                {
                    var result = new BadRequestResult();
                    context.Result = result;
                }
                else
                {
                    context.Result = context.CreateErrorResult();
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}