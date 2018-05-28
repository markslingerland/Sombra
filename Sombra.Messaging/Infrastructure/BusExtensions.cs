using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Core;

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
                return (Task<TResponse>) typedRequestMethod.Invoke(bus, new object[] {request});
            }
            catch (TimeoutException ex)
            {
                ExtendedConsole.Log($"Request {request.GetType().Name} failed. Exception: {ex}");

                return Task.FromResult((TResponse)((TResponse) Activator.CreateInstance(typeof(TResponse))).RequestFailed());
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
    }
}