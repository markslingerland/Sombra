using System;
using Sombra.IdentityService.DAL;
using Sombra.Messaging;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using System.Threading;
using System.Threading.Tasks;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System.IO;
using Microsoft.Extensions.Configuration;
using Sombra.Messaging.Infrastructure;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Infrastructure.Extensions;

namespace Sombra.IdentityService
{
    class Program
    {
        private static string _rabbitMqConnectionString;
        private static string _sqlConnectionString;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SetupConfiguration();

            var serviceProvider = new ServiceCollection()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddRequestHandlers(Assembly.GetExecutingAssembly())
                .AddDbContext<AuthenticationContext>(_sqlConnectionString)
                .BuildServiceProvider(true);

            var bus = RabbitHutch.CreateBus(_rabbitMqConnectionString);

            var responder = new AutoResponder(bus, serviceProvider);
            responder.RespondAsync(Assembly.GetExecutingAssembly());

            //Keeping the program persistent
            
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
