﻿using Microsoft.Extensions.DependencyInjection;
using Sombra.Infrastructure.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Infrastructure;
using Sombra.TemplateService.Templates.DAL;
using System;
using System.Reflection;
using System.Threading;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Sombra.TemplateService
{
    class Program
    {
        private static string _rabbitMqConnectionString;
        private static string _sqlConnectionString;
        static void Main(string[] args)
        {
            Console.WriteLine("EmailTemplate Service Started");

            SetupConfiguration();

            var serviceProvider = MessagingInstaller.Run(
                Assembly.GetExecutingAssembly(),
                _rabbitMqConnectionString,
                services => services
                    .AddDbContext<EmailTemplateContext>(_sqlConnectionString),
                ConnectionValidator.ValidateAllDbConnections, DatabaseMigrationHelper.ForceMigrations);

            Thread.Sleep(Timeout.Infinite);
        }

        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
                _sqlConnectionString = Environment.GetEnvironmentVariable("TEMPLATE_DB_CONNECTIONSTRING");
            }
            else
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
                _sqlConnectionString = config["TEMPLATE_DB_CONNECTIONSTRING"];
            }
        }
    }
}
