using System;
using Sombra.Messaging.Responses.User;

namespace Sombra.Messaging.Requests.User
{
    public class UserEmailExistsRequest : Request<UserEmailExistsResponse>
    {
        public Guid CurrentUserKey { get; set; }
        public string EmailAddress { get; set; }
    }
}