using System;
using Sombra.Messaging.Responses.User;

namespace Sombra.Messaging.Requests.User
{
    public class GetUserByKeyRequest : Request<GetUserByKeyResponse>
    {
        public Guid UserKey { get; set; }
    }
}