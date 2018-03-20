using System;

namespace Sombra.Messaging.Infrastructure
{
    public class AutoResponderRequestHandlerInfo
    {
        public readonly Type ConcreteType;
        public readonly Type InterfaceType;
        public readonly Type MessageType;
        public readonly Type ResponseType;

        public AutoResponderRequestHandlerInfo(Type concreteType, Type interfaceType, Type messageType, Type responseType)
        {
            ConcreteType = concreteType;
            InterfaceType = interfaceType;
            MessageType = messageType;
            ResponseType = responseType;
        }
    }
}