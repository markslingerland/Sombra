using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class Charity : Entity
    {
        public virtual ICollection<CharityDonation> ChartityDonations { get; set; }
    }
}