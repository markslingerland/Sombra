using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.LoggingService
{
    class Program
    {
        private static string _subscriptionIdPrefix = "Sombra.LoggingService";
        static async Task Main(string[] args)
        {
            Console.WriteLine("LoggingService started..");

            var serviceProvider = new ServiceCollection()
                .AddConsumers(Assembly.GetExecutingAssembly())
                .AddRequestHandlers(Assembly.GetExecutingAssembly())
                .AddSingleton(GetMongoCollection())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .BuildServiceProvider();

            var bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING"));
            var subscriber = new AutoSubscriber(bus, _subscriptionIdPrefix);
            subscriber.SubscribeAsync(Assembly.GetExecutingAssembly());

            bus.RespondAsync<LogRequest, LogResponse>(async request =>
            {
                var handler = new LogRequestHandler(
                    serviceProvider.GetService<IMongoCollection<LogEntry>>(),
                    serviceProvider.GetService<IMapper>());
                return await handler.Handle(request).ConfigureAwait(false);
            });

            while (true)
            {
                Thread.Sleep(10000);
            }
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
