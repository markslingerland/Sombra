using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyNetQ;
using Sombra.Messaging.Infrastructure;

namespace Sombra.Messaging.DependencyValidation
{
    public class PingResponder : AutoResponder
    {
        public PingResponder(IBus bus, IAutoResponderRequestDispatcher messageDispatcher) : base(bus, messageDispatcher)
        {
        }

        protected override void InvokeMethods(IEnumerable<KeyValuePair<Type, AutoResponderRequestHandlerInfo[]>> responderInfos, string dispatchName, MethodInfo genericBusRepondMethod, Func<Type, Type, Type> responderTypeFromRequestTypeDelegate)
        {
            foreach (var kv in responderInfos)
            {
                foreach (var responderinfo in kv.Value)
                {
                    var messageType = typeof(Ping<,>).MakeGenericType(responderinfo.MessageType, responderinfo.ResponseType);
                    var responseType = typeof(PingResponse);
                    var concreteType = typeof(PingRequestHandler<,>).MakeGenericType(messageType, responseType);

                    var dispatchMethod =
                        AutoResponderMessageDispatcher.GetType()
                            .GetMethod(dispatchName, BindingFlags.Instance | BindingFlags.Public)
                            .MakeGenericMethod(messageType, responseType, concreteType);

                    var dispatchDelegate = Delegate.CreateDelegate(responderTypeFromRequestTypeDelegate(messageType, responseType), AutoResponderMessageDispatcher, dispatchMethod);

                    var busRespondMethod = genericBusRepondMethod.MakeGenericMethod(messageType, responseType);
                    busRespondMethod.Invoke(Bus, new object[] { dispatchDelegate });
                }
            }
        }
    }
}
