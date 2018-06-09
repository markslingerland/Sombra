using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;
using System;
using System.Collections.Generic;

namespace Sombra.CharityActionService.DAL
{
    public class CharityAction : Entity
    {
        public Guid CharityActionKey { get; set; }
        public Guid CharityKey { get; set; }
        public virtual ICollection<UserKey> UserKeys { get; set; }
        public string CharityName { get; set; }
        public Category Category { get; set; }
        public string IBAN { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public bool IsApproved { get; set; }
        public string ThankYou { get; set; }

        public Guid OrganiserUserKey { get; set; }
        public string OrganiserImage { get; set; }
        public string OrganiserUserName { get; set; }

        public decimal TargetAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public DateTime ActionEndDateTime { get; set; }
    }
}