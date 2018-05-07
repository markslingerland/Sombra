using System;
using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Messaging.Infrastructure;

namespace Sombra.Messaging.DependencyValidation
{
    public class PingAutoResponder : AutoResponder
    {
        public PingAutoResponder(IBus bus, IAutoResponderRequestDispatcher messageDispatcher) : base(bus, messageDispatcher)
        {
        }

        public override void RespondAsync(params Type[] responderTypes)
        {
            var genericBusRepondMethod = GetRespondMethodOfBus(nameof(Bus.RespondAsync), typeof(Func<,>));
            var responderInfos = GetResponderInfos(responderTypes, typeof(IAsyncRequestHandler<,>));
            // ResponderInfos wrappen


            Type ResponderTypeFromRequestTypeDelegate(Type messageType, Type responseType) => typeof(Func<,>).MakeGenericType(messageType, typeof(Task<>).MakeGenericType(responseType));
            InvokeMethods(responderInfos, nameof(AutoResponderMessageDispatcher.DispatchAsync), genericBusRepondMethod, ResponderTypeFromRequestTypeDelegate);
        }
    }
}
