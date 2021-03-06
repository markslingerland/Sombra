﻿using Microsoft.Extensions.DependencyInjection;
using Sombra.Infrastructure.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Infrastructure;
using System;
using System.Reflection;
using System.Threading;
using System.IO;
using Microsoft.Extensions.Configuration;
using Sombra.CharityActionService.DAL;

namespace Sombra.CharityActionService
{
    class Program
    {
        private static string _rabbitMqConnectionString;
        private static string _sqlConnectionString;
        static void Main(string[] args)
        {
            Console.WriteLine("CharityAction Service Started");

            SetupConfiguration();

            var serviceProvider = ServiceInstaller.Run(
                Assembly.GetExecutingAssembly(),
                _rabbitMqConnectionString,
                services => services
                    .AddAutoMapper(Assembly.GetExecutingAssembly())
                    .AddDbContext<CharityActionContext>(_sqlConnectionString),
                DatabaseHelper.RunInstaller);

            Thread.Sleep(Timeout.Infinite);
        }

        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
                _sqlConnectionString = Environment.GetEnvironmentVariable("CHARITYACTION_DB_CONNECTIONSTRING");
            }
            else
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
                _sqlConnectionString = config["CHARITYACTION_DB_CONNECTIONSTRING"];
            }
        }
    }
}
