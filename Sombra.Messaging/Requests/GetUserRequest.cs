using System;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class GetUserRequest : Request<GetUserResponse>
    {
        public Guid UserKey { get; set; }
    }
}