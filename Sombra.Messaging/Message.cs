using System;
using Newtonsoft.Json;
using Sombra.Messaging.Infrastructure;

namespace Sombra.Messaging
{
    [Serializable]
    public abstract class Message : IMessage
    {
        protected Message()
        {
            MessageCreated = DateTime.UtcNow;
            MessageType = GetType().FullName;
        }
        public DateTime MessageCreated { get; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        [CacheKey]
        public string MessageType { get; }
    }
}