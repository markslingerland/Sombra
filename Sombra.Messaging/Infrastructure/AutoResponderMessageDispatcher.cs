using System.Threading.Tasks;
using EasyNetQ;

namespace Sombra.Messaging.Infrastructure
{
    public class AutoResponderMessageDispatcher
    {
        public Task DispatchAsync<TRequest, TResponse, THandler>(TRequest message)
            where TRequest : class, IRequest<TResponse>
            where THandler : IAsyncRequestHandler<TRequest, TResponse>
            where TResponse : class
        {
            var handler = (IAsyncRequestHandler<TRequest, TResponse>)ReflectionHelpers.CreateInstance<THandler>();

            return handler.Handle(message);
        }
    }
}