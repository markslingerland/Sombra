using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc.Filters;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Shared;
using Sombra.Web.Infrastructure.Messaging;

namespace Sombra.Web.Infrastructure.Filters
{
    public class ExceptionLoggingFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IBus _bus;

        public ExceptionLoggingFilterAttribute(IBus bus)
        {
            _bus = bus;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            ExtendedConsole.Log(context.Exception);
            await _bus.SendExceptionAsync(context.Exception);

            await base.OnExceptionAsync(context);
        }
    }
}