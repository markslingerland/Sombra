using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;
using Sombra.Messaging.Shared;

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

            try
            {
                consumer.Consume(message);
            }
            catch (Exception ex)
            {
                ExtendedConsole.Log(ex);
                var bus = _serviceProvider.GetRequiredService<IBus>();
                bus.Send(ServiceInstaller.ExceptionQueue, new ExceptionMessage
                {
                    Exception = ex,
                    HandlerName = nameof(consumer)
                });
            }
        }

        public async Task DispatchAsync<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : class, IConsumeAsync<TMessage>
        {
            ExtendedConsole.Log($"{typeof(TMessage).Name} received");
            var consumer = _serviceProvider.GetRequiredService<TConsumer>();

            try
            {
                await consumer.ConsumeAsync(message);
            }
            catch (Exception ex)
            {
                ExtendedConsole.Log(ex);
                var bus = _serviceProvider.GetRequiredService<IBus>();
                bus.SendAsync(ServiceInstaller.ExceptionQueue, new ExceptionMessage
                {
                    Exception = ex,
                    HandlerName = nameof(consumer)
                });
            }
        }
    }
}