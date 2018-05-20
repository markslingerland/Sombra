using Sombra.Core.Enums;
using Sombra.Messaging.Responses;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Requests
{
    public class UpdateCharityActionRequest : Request<UpdateCharityActionResponse>
    {
        public Guid CharityActionKey { get; set; }
        public Guid CharityKey { get; set; }
        public List<UserKey> UserKeys { get; set; }
        public string NameCharity { get; set; }
        public Category Category { get; set; }
        public string IBAN { get; set; }
        public string NameAction { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
    }
}