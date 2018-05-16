using System;
using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class GetCharityByKeyRequest : Request<GetCharityByKeyResponse>
    {
        public Guid CharityKey { get; set; }
    }
}