using System;
using System.Reflection;
using System.Threading;
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

            var bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING"));
            var subscriber = new AutoSubscriber(bus, _subscriptionIdPrefix);
            subscriber.SubscribeAsync(Assembly.GetExecutingAssembly());

            while (true)
            {
                Thread.Sleep(10000);
            }
        }
    }
}
