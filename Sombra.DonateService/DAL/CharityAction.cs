using System;
using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class CharityAction : Entity
    {
        public Guid CharityActionKey { get; set; }
        public string Name { get; set; }
        public DateTime ActionEndDateTime { get; set; }
        public Guid CharityId { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<CharityActionDonation> ChartityActionDonations { get; set; }
    }
}