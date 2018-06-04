using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses.Donate
{
    public class GetCharityTotalResponse : Response
    {
        public int NumberOfDonators { get; set; }
        public decimal TotalDonatedAmount { get; set; }
        public List<Donation> Donations { get; set; }
    }

    public class Donation
    {
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public decimal Amount { get; set; }
    }
}