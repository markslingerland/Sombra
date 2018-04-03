using System;
using System.Diagnostics;
using System.Reflection;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace Sombra.Messaging.Infrastructure
{
    public static class MessagingInstaller
    {
        public static ServiceProvider Run(Assembly assembly, string busConnectionString, Func<IServiceCollection, IServiceCollection> addAdditionalServices = null, Action<ServiceProvider> connectionValidator = null)
        {
            addAdditionalServices = addAdditionalServices ?? (s => s);
            var bus = RabbitHutch.CreateBus(busConnectionString).WaitForConnection();
            Console.WriteLine($"MessagingInstaller: Bus connected: {bus.IsConnected}.");

            var serviceProvider = addAdditionalServices(new ServiceCollection())
                .AddEventHandlers(assembly)
                .AddRequestHandlers(assembly)
                .AddSingleton(bus)
                .BuildServiceProvider(true);

            Console.WriteLine($"MessagingInstaller: Services are registered.");

            var responder = new AutoResponder(bus, serviceProvider);
            responder.RespondAsync(assembly);
            Console.WriteLine($"MessagingInstaller: AutoResponders initialized.");

            var subscriber = new CustomAutoSubscriber(bus, serviceProvider, assembly.FullName);
            subscriber.SubscribeAsync(assembly);
            Console.WriteLine($"MessagingInstaller: AutoSubscribers initialized.");

            if(connectionValidator != null)
            {
                var durationTracker = new Stopwatch();
                durationTracker.Start();

                Console.WriteLine($"MessagingInstaller: Running connectionValidator..");
                connectionValidator(serviceProvider);
                durationTracker.Stop();

                Console.WriteLine($"MessagingInstaller: ConnectionValidator finished running in {durationTracker.ElapsedMilliseconds}ms.");
            }

            return serviceProvider;
        }
    }
}