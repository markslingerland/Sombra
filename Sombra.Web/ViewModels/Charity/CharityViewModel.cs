using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sombra.Web.ViewModels.Charity
{
    public class CharityViewModel
    {
        public Guid CharityKey { get; set; }
        public CharityModel charity { get; set; }
        public DonationsToCharityModel donationsToCharity { get; set; }
    }
}
