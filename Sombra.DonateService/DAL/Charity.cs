using System;
using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class Charity : Entity
    {
        public Guid CharityKey { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ThankYou { get; set; }
        public virtual ICollection<CharityAction> ChartityActions{ get; set; }
        public virtual ICollection<CharityDonation> ChartityDonations { get; set; }
    }
}