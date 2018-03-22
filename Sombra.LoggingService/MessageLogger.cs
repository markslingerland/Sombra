using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Messaging;

namespace Sombra.LoggingService
{
    public class MessageLogger
    {
        private readonly IBus _bus;
        private readonly ServiceProvider _serviceProvider;
        private readonly string _subscriptionIdPrefix;

        public MessageLogger(IBus bus, ServiceProvider serviceProvider, string subscriptionIdPrefix)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
            _subscriptionIdPrefix = subscriptionIdPrefix;
        }

        public void Start()
        {
            var genericBusSubscribeMethod = GetSubscribeMethodOfBus(nameof(_bus.SubscribeAsync), typeof(Func<,>));
            Type SubscriberTypeFromMessageTypeDelegate(Type messageType) => typeof(Func<,>).MakeGenericType(messageType, typeof(Task));
            var types = GetAllMessageTypes();

            foreach (var messageType in types)
            {
                var handlerType = typeof(MessageHandler<>).MakeGenericType(messageType);
                var handler = _serviceProvider.GetRequiredService(handlerType);
                var handleMethod = handlerType.GetMethod(nameof(MessageHandler<Messaging.IMessage>.Consume),
                    BindingFlags.Instance | BindingFlags.Public);

                var busSubscribeMethod = genericBusSubscribeMethod.MakeGenericMethod(messageType);
                var handlerDelegate = Delegate.CreateDelegate(SubscriberTypeFromMessageTypeDelegate(messageType), handler, handleMethod);

                busSubscribeMethod.Invoke(_bus, new object[] { _subscriptionIdPrefix, handlerDelegate });
            }
        }

        private static MethodInfo GetSubscribeMethodOfBus(string methodName, Type parmType)
        {
            return typeof(IBus).GetMethods()
                .Where(m => m.Name == methodName)
                .Select(m => new { Method = m, Params = m.GetParameters() })
                .Single(m => m.Params.Length == 2
                             && m.Params[0].ParameterType == typeof(string)
                             && m.Params[1].ParameterType.GetGenericTypeDefinition() == parmType
                ).Method;
        }

        private static IEnumerable<Type> GetAllMessageTypes()
        {
            return typeof(Messaging.IMessage).Assembly.GetTypes()
                .Where(t => t.IsClass 
                            && !t.IsAbstract 
                            && !typeof(IRequest).IsAssignableFrom(t)
                            && !typeof(IResponse).IsAssignableFrom(t)
                            && typeof(Messaging.IMessage).IsAssignableFrom(t));
        }
    }
}