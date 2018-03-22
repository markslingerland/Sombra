using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.FluentConfiguration;

namespace Sombra.Messaging.Infrastructure
{
    public static class BusExtensions
    {
        public static async Task<TResponse> RequestAsync<TResponse>(this IBus bus, IRequest<TResponse> request)
            where TResponse : class, IResponse
        {
            return await bus.RequestAsync<IRequest<TResponse>, TResponse>(request);
        }

        public static async Task<TResponse> RequestAsync<TResponse>(this IBus bus, IRequest<TResponse> request, Action<IRequestConfiguration> configure)
            where TResponse : class, IResponse
        {
            return await bus.RequestAsync<IRequest<TResponse>, TResponse>(request, configure);
        }
    }
}