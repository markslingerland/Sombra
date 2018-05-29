using Sombra.Messaging.Responses;
using System;

namespace Sombra.Messaging.Requests
{
    public class GetCharityActionByKeyRequest : Request<GetCharityActionByKeyResponse>
    {
        public Guid CharityActionKey { get; set; }
    }
}