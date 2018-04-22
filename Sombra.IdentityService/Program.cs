using System;
using Sombra.IdentityService.DAL;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Sombra.Messaging.Infrastructure;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Infrastructure.DAL;
using Sombra.Infrastructure.Extensions;

namespace Sombra.IdentityService
{
    class Program
    {
        private static string _rabbitMqConnectionString;
        private static string _sqlConnectionString;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Identity Service Started");

            SetupConfiguration();

            var serviceProvider = MessagingInstaller.Run(
                Assembly.GetExecutingAssembly(),
                _rabbitMqConnectionString,
                services => services
                    .AddDbContext<AuthenticationContext>(_sqlConnectionString),
                ConnectionValidator.ValidateAllDbConnections, DatabaseMigrationHelper.ForceMigrations);

            Thread.Sleep(Timeout.Infinite);
        }
        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
                _sqlConnectionString = Environment.GetEnvironmentVariable("AUTHENTICATION_DB_CONNECTIONSTRING");
            }
            else
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
                _sqlConnectionString = config["AUTHENTICATION_DB_CONNECTIONSTRING"];
            }
        }
    }
}
