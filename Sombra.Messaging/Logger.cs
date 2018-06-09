using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging
{
    public static class Logger
    {
        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private static IBus Bus => (IBus) _serviceProvider.GetRequiredService(typeof(IBus));

        public static Task LogMessageAsync(IMessage message)
            => Bus.SendAsync(ServiceInstaller.LoggingQueue, message);

        public static Task LogExceptionAsync(Exception exception)
            => LogExceptionAsync(exception, null);

        public static Task LogExceptionAsync(Exception exception, string handlerName)
            => Bus.SendAsync(ServiceInstaller.ExceptionQueue, new ExceptionMessage(exception, handlerName));

        public static void LogException(Exception exception)
            => LogException(exception, null);

        public static void LogException(Exception exception, string handlerName)
            => Bus.Send(ServiceInstaller.ExceptionQueue, new ExceptionMessage(exception, handlerName));
    }
}