using System;

namespace Sombra.Messaging
{
    public interface IMessage
    {
        Guid MessageId { get; }
        DateTime MessageCreated { get; }
        string ToJson();
        string MessageType { get; }
    }
}