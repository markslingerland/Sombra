using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Sombra.Messaging.Infrastructure;

namespace Sombra.LoggingService
{
    class Program
    {
        private static string _subscriptionIdPrefix = "Sombra.LoggingService";
        private static string _rabbitMqConnectionString;
        private static string _mongoConnectionString;
        private static string _mongoDatabase;
        private static readonly string _mongoCollection = "rabbitmq";

        static async Task Main(string[] args)
        {
            Console.WriteLine("LoggingService started..");

            SetupConfiguration();

            var serviceProvider = new ServiceCollection()
                .AddConsumers(Assembly.GetExecutingAssembly())
                .AddRequestHandlers(Assembly.GetExecutingAssembly())
                .AddSingleton(GetMongoCollection())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .BuildServiceProvider();

            var bus = RabbitHutch.CreateBus(_rabbitMqConnectionString);
            var subscriber = new CustomAutoSubscriber(bus, serviceProvider, _subscriptionIdPrefix);
            subscriber.SubscribeAsync(Assembly.GetExecutingAssembly());

            var responder = new AutoResponder(bus, serviceProvider);
            responder.RespondAsync(Assembly.GetExecutingAssembly());

            //bus.RespondAsync<LogRequest, LogResponse>(async request =>
            //{
            //    var handler = new LogRequestHandler(
            //        serviceProvider.GetService<IMongoCollection<LogEntry>>(),
            //        serviceProvider.GetService<IMapper>());
            //    return await handler.Handle(request).ConfigureAwait(false);
            //});

            while (true)
            {
                Thread.Sleep(10000);
            }
        }

        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
                _mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTIONSTRING");
                _mongoDatabase = Environment.GetEnvironmentVariable("MONGO_DATABASE");
            }
            else
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
                _mongoConnectionString = config["MONGO_CONNECTIONSTRING"];
                _mongoDatabase = config["MONGO_DATABASE"];
            }
        }

        private static IMongoCollection<LogEntry> GetMongoCollection()
        {
            return new MongoClient(_mongoConnectionString).GetDatabase(_mongoDatabase).GetCollection<LogEntry>(_mongoCollection);
        }
    }
}
