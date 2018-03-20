using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;

namespace Sombra.Messaging.Infrastructure
{
    public class CustomAutoSubscriberMessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        private readonly System.IServiceProvider _serviceProvider;

        public CustomAutoSubscriberMessageDispatcher(System.IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : IConsume<TMessage>
        {
            var consumer = _serviceProvider.GetService<TConsumer>();

            consumer.Consume(message);
        }

        public Task DispatchAsync<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : IConsumeAsync<TMessage>
        {
            var consumer = _serviceProvider.GetService<TConsumer>();

            return consumer.Consume(message);
        }
    }
}