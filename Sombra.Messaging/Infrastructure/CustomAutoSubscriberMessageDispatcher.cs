using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;

namespace Sombra.Messaging.Infrastructure
{
    public class CustomAutoSubscriberMessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        private readonly ServiceProvider _serviceProvider;

        public CustomAutoSubscriberMessageDispatcher(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : class, IConsume<TMessage>
        {
            ExtendedConsole.Log($"{typeof(TMessage).Name} received");
            var consumer = _serviceProvider.GetRequiredService<TConsumer>();

            consumer.Consume(message);
        }

        public Task DispatchAsync<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : class, IConsumeAsync<TMessage>
        {
            ExtendedConsole.Log($"{typeof(TMessage).Name} received");
            var consumer = _serviceProvider.GetRequiredService<TConsumer>();

            return consumer.ConsumeAsync(message);
        }
    }
}