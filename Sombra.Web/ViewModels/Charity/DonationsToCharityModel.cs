using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sombra.Web.ViewModels.Charity
{
    public class DonationsToCharityModel
    {
        public string DonatedToCharity { get; set; }
        public string TotalDonations { get; set; }
        public string AvarageDonation { get; set; }
        public string LatestDonation { get; set; }
    }
}
