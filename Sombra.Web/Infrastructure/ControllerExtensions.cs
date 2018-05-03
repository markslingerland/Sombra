using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Sombra.Web.Infrastructure
{
    public static class ControllerExtensions
    {
        public static ContentResult CreateErrorResult(this ActionContext context)
        {
            var result = context.JsonNet(context.ModelState);
            context.HttpContext.Response.StatusCode = 400;

            return result;
        }

        public static ContentResult ModelErrors(this Controller controller, params string[] errors)
        {
            foreach (var error in errors)
            {
                controller.ModelState.AddModelError("validation-summary", error);
            }

            return controller.ControllerContext.CreateErrorResult();
        }

        public static ActionResult RedirectToActionJson(this Controller controller, string action, object routeValues)
        {
            return controller.JsonNet(new
            {
                redirect = controller.Url.Action(action, routeValues)
            });
        }

        public static ActionResult RedirectToActionJson(this Controller controller, string action, string controllerName)
        {
            return controller.JsonNet(new
            {
                redirect = controller.Url.Action(action, controllerName)
            });
        }

        public static ContentResult JsonNet(this Controller controller, object model)
        {
            return controller.ControllerContext.JsonNet(model);
        }

        public static ContentResult JsonNet(this ActionContext context, object model)
        {
            var serialized = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return new ContentResult
            {
                Content = serialized,
                ContentType = "application/json"
            };
        }
    }
}