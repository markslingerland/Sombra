using System;

namespace Sombra.Messaging
{
    public interface IMessage
    {
        DateTime Created { get; }
        string ToJson();
        string MessageType { get; }
    }
}