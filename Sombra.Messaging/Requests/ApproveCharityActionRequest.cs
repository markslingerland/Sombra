using System;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class ApproveCharityActionRequest : Request<ApproveCharityActionResponse>
    {
        public Guid CharityActionKey { get; set; }
    }
}