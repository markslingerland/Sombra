using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Sombra.Messaging.Infrastructure
{
    public class AutoResponderMessageDispatcher
    {
        private readonly ServiceProvider _serviceProvider;

        public AutoResponderMessageDispatcher(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> DispatchAsync<TRequest, TResponse, THandler>(TRequest message)
            where TRequest : class, IRequest<TResponse>
            where THandler : IAsyncRequestHandler<TRequest, TResponse>
            where TResponse : class
        {
            var handler = _serviceProvider.GetService<THandler>();

            return handler.Handle(message);
        }
    }
}