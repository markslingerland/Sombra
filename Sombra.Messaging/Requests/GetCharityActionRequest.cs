using Sombra.Core.Enums;
using Sombra.Messaging.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sombra.Messaging.Requests
{
    public class GetCharityActionRequest : Request<GetCharityActionResponse>
    {
        public Guid CharityActionkey { get; set; }
        public Guid Charitykey { get; set; }
        public ICollection UserKeys { get; set; }
        public string NameCharity { get; set; }
        public Category Category { get; set; }
        public string IBAN { get; set; }
        public string NameAction { get; set; }
        public string ActionType { get; set; }
        public string Discription { get; set; }
        public string CoverImage { get; set; }
    }
}