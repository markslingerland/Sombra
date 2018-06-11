using System;

namespace Sombra.Web.ViewModels.Charity
{
    public class DonationItemViewModel
    {
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public decimal Amount { get; set; }
    }
}