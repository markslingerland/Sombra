using System;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.Messaging.Requests.Charity
{
    public class GetCharityByKeyRequest : Request<GetCharityByKeyResponse>
    {
        public Guid CharityKey { get; set; }
    }
}