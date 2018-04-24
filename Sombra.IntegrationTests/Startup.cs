using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Timers;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;

namespace Sombra.IntegrationTests
{
    [TestClass]
    public class Startup
    {
        private const int TimeoutInMs = 600000;

        [AssemblyInitialize]
        public static void Run(TestContext context)
        {
            var timer = new System.Timers.Timer(TimeoutInMs);
            timer.Elapsed += TimerFinished;
            timer.Start();

            var items = new Dictionary<string, bool>
            {
                { "Sombra.IdentityService", false},
                { "Sombra.EmailService", false},
                { "Sombra.LoggingService", false },
                { "Sombra.TemplateService", false },
                { "Sombra.UserService", false }
            };

            var busConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
            if (string.IsNullOrEmpty(busConnectionString))
                throw new ArgumentNullException("The RabbitMQ connectionstring cannot be null or empty!");

            var bus = RabbitHutch.CreateBus(busConnectionString).WaitForConnection();
            bus.Subscribe<OnlineEvent>(Assembly.GetExecutingAssembly().GetName().Name, onlineEvent => items[onlineEvent.ServiceName] = true);
            while (!items.All(kv => kv.Value))
            {
                bus.Publish(new PublishOnlineEvent());
                Thread.Sleep(2000);
            }

            timer.Stop();
        }

        [TestMethod]
        public void T()
        {
            Assert.IsFalse(false);
        }

        private static void TimerFinished(object source, ElapsedEventArgs e)
        {
            throw new TimeoutException($"The Startup did not finishing running within the maximum designed time ({TimeoutInMs}ms)");
        }
    }
}