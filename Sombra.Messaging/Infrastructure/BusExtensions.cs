using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Core;

namespace Sombra.Messaging.Infrastructure
{
    public static class BusExtensions
    {
        public static async Task<TResponse> RequestAsync<TResponse>(this IBus bus, IRequest<TResponse> request)
            where TResponse : class, IResponse
        {
            var requestMethod = typeof(IBus).GetMethod(nameof(IBus.RequestAsync));
            var typedRequestMethod = requestMethod.MakeGenericMethod(request.GetType(), typeof(TResponse));

            try
            {
                return await (Task<TResponse>) typedRequestMethod.Invoke(bus, new object[] {request});
            }
            catch (TimeoutException ex)
            {
                ExtendedConsole.Log($"Request {request.GetType().Name} failed. Exception: {ex}");

                var responseType = typeof(TResponse);
                var response = (TResponse) Activator.CreateInstance(responseType);
                response.IsRequestSuccessful = false;

                return response;
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