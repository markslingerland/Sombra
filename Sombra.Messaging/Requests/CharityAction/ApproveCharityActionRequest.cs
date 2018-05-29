using System;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.Messaging.Requests.CharityAction
{
    public class ApproveCharityActionRequest : Request<ApproveCharityActionResponse>
    {
        public Guid CharityActionKey { get; set; }
    }
}