namespace Sombra.Messaging.Responses.Charity
{
    public class GetCharityByKeyResponse : Response
    {
        public bool Success { get; set; }
        public Shared.Charity Charity { get; set; }
    }
}