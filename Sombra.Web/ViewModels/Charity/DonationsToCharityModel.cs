using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sombra.Web.ViewModels.Charity
{
    public class DonationsToCharityModel
    {
        public int NumberOfDonators { get; set; }
        public decimal TotalDonatedAmount { get; set; }
        public decimal LastDonation { get; set; }
    }
}
