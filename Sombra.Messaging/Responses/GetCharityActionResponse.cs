using Sombra.Core.Enums;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses{
    public class GetCharityActionResponse : Response {
        public bool Success { get; set; }
        public Guid CharityActionKey { get; set; }
        public Guid CharityKey { get; set; }
        public List<UserKey> UserKeys { get; set; }
        public string CharityName { get; set; }
        public Category Category { get; set; }
        public string IBAN { get; set; }
        public string Name { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
    }

    
}

