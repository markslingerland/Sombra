namespace Sombra.Messaging.Responses.CharityAction
{
    public class GetCharityActionByKeyResponse : Response
    {
        public bool Success { get; set; }
        public Shared.CharityAction Content { get; set; }
    }
}