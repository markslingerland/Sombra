using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;

namespace Sombra.Messaging.Infrastructure
{
    public class AutoResponderRequestDispatcher : IAutoResponderRequestDispatcher
    {
        private readonly ServiceProvider _serviceProvider;

        public AutoResponderRequestDispatcher(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> DispatchAsync<TRequest, TResponse, THandler>(TRequest message)
            where TRequest : class, IRequest<TResponse>
            where THandler : IAsyncRequestHandler<TRequest, TResponse>
            where TResponse : class, IResponse
        {
            ExtendedConsole.Log($"{typeof(TRequest).Name} received");
            var handler = _serviceProvider.GetRequiredService<THandler>();

            var response = await handler.Handle(message);
            ExtendedConsole.Log($"{typeof(TResponse).Name} returned");
            _serviceProvider.GetRequiredService<IBus>().SendAsync(ServiceInstaller.LoggingQueue, response);

            return response;
        }
    }
}