using Sombra.Core.Enums;
using Sombra.Messaging.Responses;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Requests
{
    public class GetCharityActionRequest : Request<GetCharityActionResponse>
    {
        public Guid CharityActionKey { get; set; }
    }
}