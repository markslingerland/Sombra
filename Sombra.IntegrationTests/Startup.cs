using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Timers;
using EasyNetQ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;

namespace Sombra.IntegrationTests
{
    [TestClass]
    public class Startup
    {
        private const int TimeoutInMs = 600000;

        public static List<Assembly> Assemblies = new List<Assembly>
        {
            typeof(IdentityService.Program).Assembly,
            typeof(EmailService.Program).Assembly,
            typeof(TemplateService.Program).Assembly,
            typeof(UserService.Program).Assembly,
        };

        [AssemblyInitialize]
        public static void Run(TestContext context)
        {
            var timer = new System.Timers.Timer(TimeoutInMs);
            timer.Elapsed += TimerFinished;
            timer.Start();

            var items = Assemblies.ToDictionary(assembly => assembly.GetName().Name, assembly => false);

            Environment.SetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING", "host=localhost;username=rabbitmquser;password=rabbitmqpassword");
            Environment.SetEnvironmentVariable("CONTAINER_TYPE", "Docker");
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
        private static TestServer _server;
        private static HttpClient _client;

        [TestInitialize]
        public void InitTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Web.Startup>());
            _client = _server.CreateClient();
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