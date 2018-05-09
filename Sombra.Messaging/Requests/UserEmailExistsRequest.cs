using System;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class UserEmailExistsRequest : Request<UserEmailExistsResponse>
    {
        public Guid CurrentUserKey { get; set; }
        public string EmailAddress { get; set; }
    }
}