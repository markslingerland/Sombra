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
            ExtendedConsole.Log(ex);
            await _bus.SendAsync(ServiceInstaller.ExceptionQueue, new ExceptionMessage
            {
                Exception = ex
            });

            await base.OnExceptionAsync(context);
        }
    }
}