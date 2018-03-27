using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Sombra.Messaging.Infrastructure;

namespace Sombra.EmailService
{
    class Program
    {
        private static string _rabbitMqConnectionString;

        static async Task Main(string[] args)
        {
            Console.WriteLine("EmailService started..");

            // Laad de connectionstring in
            SetupConfiguration();

            // Maak een bus aan, registreer alle handlers en start de autosubscriber en autoresponder
            var serviceProvider = MessagingInstaller.Run(Assembly.GetExecutingAssembly(), _rabbitMqConnectionString);

            // Laat de applicatie runnen
            Thread.Sleep(Timeout.Infinite);
        }

        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                // Docker container. Haal de variabelen uit de environment (docker-compose-override)
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
            }
            else
            {
                // Geen container. Haal ze uit de appsettings.json
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
            }
        }
    }
}
