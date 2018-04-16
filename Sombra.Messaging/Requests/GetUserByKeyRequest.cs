using System;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class GetUserByKeyRequest : Request<GetUserByKeyResponse>
    {
        public Guid UserKey { get; set; }
    }
}