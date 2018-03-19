using System;
using Newtonsoft.Json;

namespace Sombra.Messaging
{
    [Serializable]
    public abstract class Message
    {
        protected Message()
        {
            Created = DateTime.UtcNow;
        }
        public DateTime Created { get; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}