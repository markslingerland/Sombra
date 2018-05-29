using System;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.Messaging.Requests.CharityAction
{
    public class GetCharityActionRequest : Request<GetCharityActionResponse>
    {
        public Guid CharityActionKey { get; set; }
    }
}