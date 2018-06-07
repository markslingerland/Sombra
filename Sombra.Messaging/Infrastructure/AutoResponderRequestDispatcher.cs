using System;
using System.Threading.Tasks;
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
            ExtendedConsole.Log($"{message.GetType().Name} received");
            var handler = _serviceProvider.GetRequiredService<THandler>();

            try
            {
                var response = await handler.Handle(message);
                ExtendedConsole.Log($"{response.GetType().Name} returned");
                Logger.LogMessageAsync(response);
            }
            catch (Exception ex)
            {
                ExtendedConsole.Log(ex);
                Logger.LogExceptionAsync(ex, handler.GetType().Name);
            }

            return (TResponse) Activator.CreateInstance<TResponse>().RequestFailed();
        }
    }
}