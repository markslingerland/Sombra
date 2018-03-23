using System.Threading.Tasks;
using EasyNetQ;

namespace Sombra.Messaging.Infrastructure
{
    public static class BusExtensions
    {
        public static async Task<TResponse> RequestAsync<TResponse>(this IBus bus, IRequest<TResponse> request)
            where TResponse : class, IResponse
        {
            return await bus.RequestAsync<IRequest<TResponse>, TResponse>(request);
        }
    }
}