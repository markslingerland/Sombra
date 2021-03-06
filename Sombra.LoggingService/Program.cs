﻿using System;
using System.IO;
using System.Reflection;
using System.Threading;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;
using Sombra.Infrastructure.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Shared;

namespace Sombra.LoggingService
{
    class Program
    {
        private static string _rabbitMqConnectionString;
        private static string _mongoConnectionString;
        private static string _mongoDatabase;

        static void Main(string[] args)
        {
            ExtendedConsole.Log("LoggingService started..");

            SetupConfiguration();

            var serviceProvider = ServiceInstaller.Run(
                Assembly.GetExecutingAssembly(),
                _rabbitMqConnectionString,
                services => services
                    .AddAutoMapper(Assembly.GetExecutingAssembly())
                    .AddMongoDatabase(_mongoConnectionString, _mongoDatabase)
                    .AddTransient<MessageHandler>()
                    .AddTransient<ExceptionMessageHandler>()
                    .AddTransient<WebRequestMessageHandler>(),
                DatabaseHelper.ValidateMongoConnections);

            var bus = serviceProvider.GetRequiredService<IBus>();
            var logger = new MessageLogger(bus, serviceProvider, "MessageLoggerSubscription");
            logger.Start();

            bus.Receive<ExceptionMessage>(ServiceInstaller.ExceptionQueue, async message =>
            {
                var handler = serviceProvider.GetRequiredService<ExceptionMessageHandler>();
                await handler.HandleAsync(message);
            });

            bus.Receive<WebRequest>(ServiceInstaller.WebRequestsQueue, async message =>
            {
                var handler = serviceProvider.GetRequiredService<WebRequestMessageHandler>();
                await handler.HandleAsync(message);
            });

            Thread.Sleep(Timeout.Infinite);
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
    }
}