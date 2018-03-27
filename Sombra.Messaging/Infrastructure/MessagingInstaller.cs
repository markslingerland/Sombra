using System;
using System.Reflection;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace Sombra.Messaging.Infrastructure
{
    public static class MessagingInstaller
    {
        public static ServiceProvider Run(Assembly assembly, string busConnectionString, Func<IServiceCollection, IServiceCollection> addAdditionalServices = null)
        {
            addAdditionalServices = addAdditionalServices ?? (s => s);
            var bus = RabbitHutch.CreateBus(busConnectionString).WaitForConnection();

            var serviceProvider = addAdditionalServices(new ServiceCollection())
                .AddEventHandlers(assembly)
                .AddRequestHandlers(assembly)
                .AddSingleton(bus)
                .BuildServiceProvider(true);

            var responder = new AutoResponder(bus, serviceProvider);
            responder.RespondAsync(assembly);

            var subscriber = new CustomAutoSubscriber(bus, serviceProvider, assembly.FullName);
            subscriber.SubscribeAsync(assembly);

            return serviceProvider;
        }
    }
}