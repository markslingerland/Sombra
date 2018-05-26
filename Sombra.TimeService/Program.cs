using System;
using System.IO;
using System.Reflection;
using System.Threading;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Infrastructure.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Infrastructure;
using Sombra.TimeService.DAL;

namespace Sombra.TimeService
{
    class Program
    {
        private static string _rabbitMqConnectionString;
        private static string _sqlConnectionString;
        static void Main(string[] args)
        {
            Console.WriteLine("Time Service Started");

            SetupConfiguration();

            var serviceProvider = ServiceInstaller.Run(
                Assembly.GetExecutingAssembly(),
                _rabbitMqConnectionString,
                services => services
                    .AddAutoMapper(Assembly.GetExecutingAssembly())
                    .AddDbContext<TimeContext>(_sqlConnectionString)
                    .AddSingleton<TimeManager>(),
                ConnectionValidator.ValidateAllDbConnections, DatabaseMigrationHelper.ForceMigrations);

            var manager = serviceProvider.GetRequiredService<TimeManager>();
            manager.Start();

            Thread.Sleep(Timeout.Infinite);
        }
        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
                _sqlConnectionString = Environment.GetEnvironmentVariable("TIME_DB_CONNECTIONSTRING");
            }
            else
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
                _sqlConnectionString = config["TIME_DB_CONNECTIONSTRING"];
            }
        }
    }
}