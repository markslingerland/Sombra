using System;

namespace Sombra.Messaging.Responses
{
    public class CreateUserResponse : Response
    {
        public Guid UserKey { get; set; }
        public bool Success { get; set; }
    }
}