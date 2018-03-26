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
using FizzWare.NBuilder;
using System.Linq;

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

            using (var serviceScope = serviceProvider.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<AuthenticationContext>();
                // Seed the database.

                //Generate roles
                var roles = Builder<Role>.CreateListOfSize(10)
                    .Build();

                db.Roles.AddRange(roles.ToArray());

                //Generate Users
                var users = Builder<User>.CreateListOfSize(10)
                    .Build();

                db.Users.AddRange(users.ToArray());

                //Generate Permissions
                var permissions = Builder<Permission>.CreateListOfSize(10)
                    .Build();

                db.Permissions.AddRange(permissions.ToArray());

                //Generate credentialTypes
                var credentialTypes = Builder<CredentialType>.CreateListOfSize(10)
                    .Build();

                db.CredentialTypes.AddRange(credentialTypes.ToArray());

                //Generate Permissions
                var credentials = Builder<Credential>.CreateListOfSize(10)
                    .TheFirst(1)
                    .With(x => x.Secret = Encryption.CreateHash("admin"))
                    .With(x => x.Identifier = "admin")
                    .Build();
                    

                db.Credentials.AddRange(credentials.ToArray());

                for(var i = 0; i < 10; i++) {
                    db.RolePermissions.Add(new RolePermission {
                        Role = roles[i],
                        Permission = permissions[i]
                    });
                }

                for(var i = 0; i < 10; i++){
                    db.UserRoles.Add(new UserRole{
                        Role = roles[i],
                        User = users[i]
                    });
                }                

                db.SaveChanges();
            }
            

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
