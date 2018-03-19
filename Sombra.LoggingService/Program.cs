using System;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Sombra.LoggingService
{
    class Program
    {
        private static string _subscriptionIdPrefix = "Sombra.LoggingService";
        static async Task Main(string[] args)
        {
            Console.WriteLine("LoggingService started..");

            var serviceProvider = new ServiceCollection()
                .AddSingleton(GetMongoCollection())
                .BuildServiceProvider();

            var bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING"));
            var subscriber = new AutoSubscriber(bus, _subscriptionIdPrefix);
            subscriber.SubscribeAsync(Assembly.GetExecutingAssembly());
        }

        private static IMongoCollection<LogEntry> GetMongoCollection()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTIONSTRING");
            var db = Environment.GetEnvironmentVariable("MONGO_DATABASE");
            var coll = Environment.GetEnvironmentVariable("MONGO_COLLECTION");

            return new MongoClient(connectionString).GetDatabase(db).GetCollection<LogEntry>(coll);
        }
    }
}
