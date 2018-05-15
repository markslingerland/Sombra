using Sombra.Core.Enums;
using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses
{
    public class UpdateCharityActionResponse : Response
    {
        public UpdateCharityActionResponse() { }
        public UpdateCharityActionResponse(bool success)
        {
            Success = success;

        }
        public bool Success { get; set; }
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