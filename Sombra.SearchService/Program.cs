﻿using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Sombra.Infrastructure.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Infrastructure;

namespace Sombra.SearchService
{
    class Program
    {
        private static string _rabbitMqConnectionString;
        private static string _sqlConnectionString;
        static void Main(string[] args)
        {
            Console.WriteLine("Search Service Started");

            SetupConfiguration();

            var serviceProvider = MessagingInstaller.Run(
                Assembly.GetExecutingAssembly(),
                _rabbitMqConnectionString,
                services => services
                    .AddDbContext<>(_sqlConnectionString),
                ConnectionValidator.ValidateAllDbConnections, DatabaseMigrationHelper.ForceMigrations);

            Thread.Sleep(Timeout.Infinite);
        }

        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
                _sqlConnectionString = Environment.GetEnvironmentVariable("SEARCH_DB_CONNECTIONSTRING");
            }
            else
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
                _sqlConnectionString = config["SEARCH_DB_CONNECTIONSTRING"];
            }
        }
    }
}
