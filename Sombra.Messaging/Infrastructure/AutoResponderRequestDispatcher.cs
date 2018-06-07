using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Infrastructure
{
    public class AutoResponderRequestDispatcher : IAutoResponderRequestDispatcher
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IBus _bus;

        public AutoResponderRequestDispatcher(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _bus = serviceProvider.GetRequiredService<IBus>();
        }

        public async Task<TResponse> DispatchAsync<TRequest, TResponse, THandler>(TRequest message)
            where TRequest : class, IRequest<TResponse>
            where THandler : IAsyncRequestHandler<TRequest, TResponse>
            where TResponse : class, IResponse
        {
            ExtendedConsole.Log($"{nameof(message)} received");
            var handler = _serviceProvider.GetRequiredService<THandler>();

            try
            {
                var response = await handler.Handle(message);
                ExtendedConsole.Log($"{nameof(response)} returned");
                _bus.SendMessageLogAsync(response);
            }
            catch (Exception ex)
            {
                ExtendedConsole.Log(ex);
                _bus.SendExceptionAsync(ex, nameof(handler));
            }

            return (TResponse) Activator.CreateInstance<TResponse>().RequestFailed();
        }
    }
}