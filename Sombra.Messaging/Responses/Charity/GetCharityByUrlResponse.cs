namespace Sombra.Messaging.Responses.Charity
{
    public class GetCharityByUrlResponse : Response
    {
        public bool IsSuccess { get; set; }

        public Shared.Charity Charity { get; set; }
    }
}