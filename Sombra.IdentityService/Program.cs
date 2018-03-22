using System;
using Sombra.IdentityService.DAL;
using Sombra.Messaging;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using System.Threading;
using System.Threading.Tasks;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.IdentityService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var context = new AuthenticationContext();
            var bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING"));
            bus.RespondAsync<UserLoginRequest, UserLoginResponse>(async request => {
                // TODO: Deze shit ga ik proberen weg te werken
                var handler = new UserLoginRequestHandler(context);
                return await handler.Handle(request).ConfigureAwait(false);
            });



            //Keeping the program persistent
            while (true)
            {
                Thread.Sleep(10000);
            }
        }
    }
}
