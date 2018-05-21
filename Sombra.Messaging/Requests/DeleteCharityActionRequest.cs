using Sombra.Core.Enums;
using Sombra.Messaging.Responses;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Requests
{
    public class DeleteCharityActionRequest : Request<DeleteCharityActionResponse>
    {
        public Guid CharityActionKey { get; set; }
    }
}