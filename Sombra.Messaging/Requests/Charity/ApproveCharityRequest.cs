using System;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.Messaging.Requests.Charity
{
    public class ApproveCharityRequest : Request<ApproveCharityResponse>
    {
        public Guid CharityKey { get; set; }
    }
}