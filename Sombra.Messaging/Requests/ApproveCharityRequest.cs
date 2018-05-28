using System;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class ApproveCharityRequest : Request<ApproveCharityResponse>
    {
        public Guid CharityKey { get; set; }
    }
}