using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Sombra.Core;
using Sombra.Messaging;

namespace Sombra.Web.Infrastructure.Filters
{
    public class ExceptionLoggingFilterAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            ExtendedConsole.Log(context.Exception);
            await Logger.LogExceptionAsync(context.Exception);

            await base.OnExceptionAsync(context);
        }
    }
}