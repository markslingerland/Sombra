namespace Sombra.Messaging.Responses.Donate
{
    public class GetCharityTotalResponse : Response
    {
        public int NumberOfDonators { get; set; }
        public decimal TotalDonatedAmount { get; set; }
        public decimal LastDonation { get; set; }
    }
}