using System;
using Newtonsoft.Json;

namespace Sombra.Messaging
{
    [Serializable]
    public abstract class Message : IMessage
    {
        protected Message()
        {
            Created = DateTime.UtcNow;
            MessageType = GetType().FullName;
        }
        public DateTime Created { get; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string MessageType { get; }
    }
}