using System;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;

namespace Sombra.EmailService
{
    class Program
    {
        private static string _subscriptionIdPrefix = "Sombra.EmailService";
        static async Task Main(string[] args)
        {
            Console.WriteLine("EmailService started..");

            var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
            var rabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQ_USER");
            var rabbitMqPassword = Environment.GetEnvironmentVariable("DEBmbwkSrzy9D1T9cJfa");

            var bus = RabbitHutch.CreateBus($"host={rabbitMqHost};username={rabbitMqUser};password={rabbitMqPassword}");
            var subscriber = new AutoSubscriber(bus, _subscriptionIdPrefix);
            subscriber.SubscribeAsync(Assembly.GetExecutingAssembly());
        }
    }
}
