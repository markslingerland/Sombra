using System;

namespace Sombra.Messaging
{
    public interface IMessage
    {
        DateTime MessageCreated { get; }
        string ToJson();
        string MessageType { get; }
    }
}