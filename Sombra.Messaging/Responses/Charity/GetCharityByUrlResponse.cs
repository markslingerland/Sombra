namespace Sombra.Messaging.Responses.Charity
{
    public class GetCharityByUrlResponse : Response
    {
        public bool Success { get; set; }
        public Shared.Charity Charity { get; set; }
    }
}