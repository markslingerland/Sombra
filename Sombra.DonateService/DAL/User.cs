using System;
using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class User : Entity
    {
        public Guid UserKey { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public virtual ICollection<CharityActionDonation> CharityActionDonations { get; set; }
        public virtual ICollection<CharityDonation> CharityDonations { get; set; }
    }
}