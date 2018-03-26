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
using Sombra.Core;

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

            var serviceProvider = new ServiceCollection()
                .AddRequestHandlers(Assembly.GetExecutingAssembly())
                .AddDbContext<AuthenticationContext>(_sqlConnectionString)
                .BuildServiceProvider(true);

            var db = serviceProvider.GetRequiredService<AuthenticationContext>();
            var user = new Credential {
              Secret = Encryption.CreateHash("newpassword"),
              CredentialTypeId = new Guid(),
              Identifier = "test",
              UserId = new Guid(),
              
              
            };

            db.SaveChanges();

            var bus = RabbitHutch.CreateBus(_rabbitMqConnectionString).WaitForConnection();

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
