using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;
using System;
using System.Collections.Generic;

namespace Sombra.CharityActionService.DAL
{
    public class CharityAction : Entity
    {
        // TODO at more data relevant for charityAction
        public Guid CharityActionKey { get; set; }
        public Guid CharityKey { get; set; }    
        public virtual ICollection<UserKey> UserKeys { get; set; }
        public string NameCharity { get; set; }
        public Category Category { get; set; }
        public string IBAN { get; set; }
        public string NameAction { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }

    }
}
