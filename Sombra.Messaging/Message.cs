using System;

namespace Sombra.Messaging
{
    [Serializable]
    public abstract class Message
    {
        protected Message()
        {
            Created = DateTime.UtcNow;
        }
        protected DateTime Created { get; }
    }
}