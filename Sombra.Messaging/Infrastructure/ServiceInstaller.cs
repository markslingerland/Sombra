using System;
using System.Diagnostics;
using System.Reflection;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;
using Sombra.Messaging.DependencyValidation;

namespace Sombra.Messaging.Infrastructure
{
    public static class ServiceInstaller
    {
        public const string LoggingQueue = "IMessageLoggingQueue";
        public const string ExceptionQueue = "ExceptionQueue";
        public const string WebRequestsQueue = "WebRequestsQueue";

        public static ServiceProvider Run(Assembly assembly, string busConnectionString, Func<IServiceCollection, IServiceCollection> addAdditionalServices = null, params Action<ServiceProvider>[] additionalActions)
        {
            var installerStopwatch = new Stopwatch();
            installerStopwatch.Start();

            addAdditionalServices = addAdditionalServices ?? (s => s);

            var bus = RabbitHutch.CreateBus(busConnectionString).WaitForConnection();
            ExtendedConsole.Log($"ServiceInstaller: Bus connected: {bus.IsConnected}.");

            var serviceProvider = addAdditionalServices(new ServiceCollection())
                .AddEventHandlers(assembly)
                .AddRequestHandlers(assembly)
                .AddPingRequestHandlers(assembly)
                .AddSingleton(bus)
                .BuildServiceProvider(true);

            ExtendedConsole.Log("ServiceInstaller: Services are registered.");

            Logger.SetServiceProvider(serviceProvider);
            ExtendedConsole.Log("ServiceInstaller: Logger initialized");

            var responder = new AutoResponder(bus, new AutoResponderRequestDispatcher(serviceProvider));
            responder.RespondAsync(assembly);
            ExtendedConsole.Log("ServiceInstaller: AutoResponders initialized.");

            var pingResponder = new PingResponder(bus, new AutoResponderRequestDispatcher(serviceProvider));
            pingResponder.RespondAsync(assembly);
            ExtendedConsole.Log("ServiceInstaller: PingResponder initialized.");

            var subscriber = new CustomAutoSubscriber(bus, new CustomAutoSubscriberMessageDispatcher(serviceProvider), assembly.FullName);
            subscriber.SubscribeAsync(assembly);
            ExtendedConsole.Log("ServiceInstaller: AutoSubscribers initialized.");

            if(additionalActions != null)
            {
                foreach (var additionalAction in additionalActions)
                {
                    var additionalActionStopwatch = new Stopwatch();
                    additionalActionStopwatch.Start();

                    ExtendedConsole.Log($"ServiceInstaller: Running {additionalAction.Method.Name}");
                    additionalAction(serviceProvider);
                    additionalActionStopwatch.Stop();

                    ExtendedConsole.Log($"ServiceInstaller: {additionalAction.Method.Name} finished running in {additionalActionStopwatch.ElapsedMilliseconds}ms.");
                }
            }

            installerStopwatch.Stop();
            ExtendedConsole.Log($"ServiceInstaller: Finished running in {installerStopwatch.ElapsedMilliseconds}ms.");

            return serviceProvider;
        }
    }
}