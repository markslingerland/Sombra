using System;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.Messaging.Requests.CharityAction
{
    public class GetCharityActionByKeyRequest : Request<GetCharityActionByKeyResponse>
    {
        public Guid CharityActionKey { get; set; }
    }
}