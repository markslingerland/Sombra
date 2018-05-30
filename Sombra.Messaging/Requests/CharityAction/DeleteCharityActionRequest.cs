using System;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.Messaging.Requests.CharityAction
{
    public class DeleteCharityActionRequest : Request<DeleteCharityActionResponse>
    {
        public Guid CharityActionKey { get; set; }
    }
}