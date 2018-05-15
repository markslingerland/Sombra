using Sombra.Core.Enums;
using Sombra.Messaging.Responses;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Requests
{
    public class UpdateCharityActionRequest : Request<UpdateCharityActionResponse>
    {
        public Guid CharityActionkey { get; set; }
        public Guid Charitykey { get; set; }
        public ICollection<Guid> UserKeys { get; set; }
        public string NameCharity { get; set; }
        public Category Category { get; set; }
        public string IBAN { get; set; }
        public string NameAction { get; set; }
        public string ActionType { get; set; }
        public string Discription { get; set; }
        public string CoverImage { get; set; }
    }
}