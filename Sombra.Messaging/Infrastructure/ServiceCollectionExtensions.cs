using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Messaging.DependencyValidation;

namespace Sombra.Messaging.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return serviceCollection.AddGenericInterfaceType(assembly, typeof(IAsyncEventHandler<>));
        }

        public static IServiceCollection AddRequestHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return serviceCollection.AddGenericInterfaceType(assembly, typeof(IAsyncRequestHandler<,>));
        }

        public static IServiceCollection AddPingRequestHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return GetGenericInterfaceTypes(assembly, typeof(IAsyncRequestHandler<,>)).Aggregate(serviceCollection,
                (current, t) =>
                {
                    var genericArguments = t.GetInterface(typeof(IAsyncRequestHandler<,>).Name).GetGenericArguments();
                    var requestType = genericArguments[0];
                    var responseType = genericArguments[1];

                    var pingRequestType = typeof(Ping<,>).MakeGenericType(requestType, responseType);
                    var pingResponseType = typeof(PingResponse<>).MakeGenericType(responseType);
                    var pingHandlerType = typeof(PingRequestHandler<,,>).MakeGenericType(pingRequestType, pingResponseType, responseType);

                    return current.AddTransient(pingHandlerType);
                });
        }

        private static IServiceCollection AddGenericInterfaceType(this IServiceCollection serviceCollection, Assembly assembly, Type type)
        {
            if (!type.IsInterface || !type.IsGenericType) throw new ArgumentException($"The supplied type must be a generic interface. Current type: {type}", nameof(type));

            return GetGenericInterfaceTypes(assembly, type).Aggregate(serviceCollection, (current, t) => current.AddTransient(t));
        }

        private static IEnumerable<Type> GetGenericInterfaceTypes(Assembly assembly, Type type)
        {
            return assembly.GetTypes()
                .Where(x =>
                    x.GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == type) &&
                    !x.IsInterface && !x.IsAbstract);
        }
    }
}