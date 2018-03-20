using System.Linq;
using System.Reflection;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;

namespace Sombra.Messaging.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsumers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            assembly.GetTypes()
                .Where(x => x.GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(IConsumeAsync<>)) && !x.IsInterface && !x.IsAbstract)
                .Select(serviceCollection.AddTransient);

            return serviceCollection;
        }

        public static IServiceCollection AddRequestHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            assembly.GetTypes()
                .Where(x => x.GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(IAsyncRequestHandler<,>)) && !x.IsInterface && !x.IsAbstract)
                .Select(serviceCollection.AddTransient);

            return serviceCollection;
        }
    }
}