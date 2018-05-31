using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class CharityAction : Entity
    {
        public virtual ICollection<CharityActionDonation> ChartityActionDonations { get; set; }
    }
}