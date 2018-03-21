using System;
using System.Linq;
using System.Reflection;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;

namespace Sombra.Messaging.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return serviceCollection.AddGenericInterfaceType(assembly, typeof(IAsyncMessageHandler<>));
        }

        public static IServiceCollection AddRequestHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return serviceCollection.AddGenericInterfaceType(assembly, typeof(IAsyncRequestHandler<,>));
        }

        public static IServiceCollection AddGenericInterfaceType(this IServiceCollection serviceCollection, Assembly assembly, Type type)
        {
            if (!type.IsInterface || !type.IsGenericType) throw new ArgumentException($"The supplied type must be a generic interface. Current type: {type}", nameof(type));

            return assembly.GetTypes()
                .Where(x => x.GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == type) && !x.IsInterface && !x.IsAbstract)
                .Aggregate(serviceCollection, (current, t) => current.AddTransient(t));
        }
    }
}