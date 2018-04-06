using System;
using System.Diagnostics;
using System.Reflection;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;

namespace Sombra.Messaging.Infrastructure
{
    public static class MessagingInstaller
    {
        public static ServiceProvider Run(Assembly assembly, string busConnectionString, Func<IServiceCollection, IServiceCollection> addAdditionalServices = null, Action<ServiceProvider> additionalAction = null)
        {
            var installerStopwatch = new Stopwatch();
            installerStopwatch.Start();

            addAdditionalServices = addAdditionalServices ?? (s => s);
            var bus = RabbitHutch.CreateBus(busConnectionString).WaitForConnection();
            ExtendedConsole.Log($"MessagingInstaller: Bus connected: {bus.IsConnected}.");

            var serviceProvider = addAdditionalServices(new ServiceCollection())
                .AddEventHandlers(assembly)
                .AddRequestHandlers(assembly)
                .AddSingleton(bus)
                .BuildServiceProvider(true);

            ExtendedConsole.Log("MessagingInstaller: Services are registered.");

            var responder = new AutoResponder(bus, serviceProvider);
            responder.RespondAsync(assembly);
            ExtendedConsole.Log("MessagingInstaller: AutoResponders initialized.");

            var subscriber = new CustomAutoSubscriber(bus, serviceProvider, assembly.FullName);
            subscriber.SubscribeAsync(assembly);
            ExtendedConsole.Log("MessagingInstaller: AutoSubscribers initialized.");

            if(additionalAction != null)
            {
                var additionalActionStopwatch = new Stopwatch();
                additionalActionStopwatch.Start();

                ExtendedConsole.Log($"MessagingInstaller: Running {nameof(additionalAction)}");
                additionalAction(serviceProvider);
                additionalActionStopwatch.Stop();

                ExtendedConsole.Log($"MessagingInstaller: {nameof(additionalAction)} finished running in {additionalActionStopwatch.ElapsedMilliseconds}ms.");
            }

            installerStopwatch.Stop();
            ExtendedConsole.Log($"MessagingInstaller: Finished running in {installerStopwatch.ElapsedMilliseconds}ms.");

            return serviceProvider;
        }
    }
}