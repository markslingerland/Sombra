using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Core;

namespace Sombra.Messaging.Infrastructure
{
    public static class BusExtensions
    {
        private static readonly MethodInfo _genericRequestMethod = typeof(IBus).GetMethods().Single(m => m.Name == nameof(IBus.RequestAsync) && m.GetParameters().Length == 1);
        private static readonly ConcurrentDictionary<Type, MethodInfo> _typedRequestMethods = new ConcurrentDictionary<Type, MethodInfo>();

        public static Task<TResponse> RequestAsync<TResponse>(this IBus bus, IRequest<TResponse> request)
            where TResponse : class, IResponse, new()
        {
            var requestMethod = _typedRequestMethods.GetOrAdd(request.GetType(),
                requestType => _genericRequestMethod.MakeGenericMethod(requestType, typeof(TResponse)));

            try
            {
                Logger.LogMessageAsync(request);
                return (Task<TResponse>)requestMethod.Invoke(bus, new object[] {request});
            }
            catch (TimeoutException ex)
            {
                ExtendedConsole.Log($"Request {request.GetType().Name} failed. Exception: {ex}");
                Logger.LogExceptionAsync(ex, true);

                return Task.FromResult((TResponse)new TResponse().RequestFailed());
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