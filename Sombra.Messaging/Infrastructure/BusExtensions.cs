using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Core;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Infrastructure
{
    public static class BusExtensions
    {
        public static Task<TResponse> RequestAsync<TResponse>(this IBus bus, IRequest<TResponse> request)
            where TResponse : class, IResponse
        {
            var requestMethod = typeof(IBus).GetMethods().Single(m => m.Name == nameof(IBus.RequestAsync) && m.GetParameters().Length == 1);
            var typedRequestMethod = requestMethod.MakeGenericMethod(request.GetType(), typeof(TResponse));

            try
            {
                bus.SendMessageLogAsync(request);
                return (Task<TResponse>) typedRequestMethod.Invoke(bus, new object[] {request});
            }
            catch (TimeoutException ex)
            {
                ExtendedConsole.Log($"Request {request.GetType().Name} failed. Exception: {ex}");

                return Task.FromResult((TResponse)Activator.CreateInstance<TResponse>().RequestFailed());
            }
        }

        public static IBus WaitForConnection(this IBus bus, int retryInMs = 2500)
        {
            while (!bus.IsConnected)
            {
                Thread.Sleep(retryInMs);
                ExtendedConsole.Log("IBus.WaitForConnection: Waiting for bus to come online..");
            }

            return bus;
        }

        public static Task SendMessageLogAsync(this IBus bus, IMessage message)
            => bus.SendAsync(ServiceInstaller.LoggingQueue, message);

        public static Task SendExceptionAsync(this IBus bus, Exception exception)
            => bus.SendExceptionAsync(exception, null);

        public static Task SendExceptionAsync(this IBus bus, Exception exception, string handlerName)
            => bus.SendAsync(ServiceInstaller.ExceptionQueue, new ExceptionMessage(exception, handlerName));

        public static void SendException(this IBus bus, Exception exception)
            => bus.SendException(exception, null);

        public static void SendException(this IBus bus, Exception exception, string handlerName)
            => bus.Send(ServiceInstaller.ExceptionQueue, new ExceptionMessage(exception, handlerName));
    }
}