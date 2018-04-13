using Microsoft.Extensions.DependencyInjection;
using Sombra.Messaging.Infrastructure;
using System;
using System.Reflection;
using System.Threading;

namespace Sombra.TemplateService
{
    class Program
    {
        private static string _rabbitMqConnectionString;

        static void Main(string[] args)
        {
            Console.WriteLine("TemplateService started..");

            SetupConfiguration();
            var serviceProvider = MessagingInstaller.Run(
                Assembly.GetExecutingAssembly(),
                _rabbitMqConnectionString);

            Thread.Sleep(Timeout.Infinite);
        }

        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
            }
            else
            {

            }
        }
    }
}
