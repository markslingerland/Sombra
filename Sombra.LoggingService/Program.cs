using System;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;

namespace Sombra.LoggingService
{
    class Program
    {
        private static string _subscriptionIdPrefix = "Sombra.LoggingService";
        static async Task Main(string[] args)
        {
            Console.WriteLine("LoggingService started..");

            var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
            var rabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQ_USER");
            var rabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");

            var bus = RabbitHutch.CreateBus($"host={rabbitMqHost};username={rabbitMqUser};password={rabbitMqPassword}");
            var subscriber = new AutoSubscriber(bus, _subscriptionIdPrefix);
            subscriber.SubscribeAsync(Assembly.GetExecutingAssembly());
        }
    }
}
