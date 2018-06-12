using System.Collections.Generic;

namespace Sombra.Web.ViewModels.Charity
{
    public class DonationsViewModel
    {
        public List<DonationItemViewModel> Donations { get; set; }
        public int NumberOfDonators { get; set; }
        public decimal TotalDonatedAmount { get; set; }
    }
}