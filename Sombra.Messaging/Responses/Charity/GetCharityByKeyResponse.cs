namespace Sombra.Messaging.Responses.Charity
{
    public class GetCharityByKeyResponse : Response
    {
        public bool IsSuccess { get; set; }
        public Shared.Charity Charity { get; set; }
    }
}