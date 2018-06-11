namespace Sombra.Messaging.Responses.CharityAction
{
    public class GetCharityActionByUrlResponse : Response
    {
        public bool IsSuccess { get; set; }
        public Shared.CharityAction CharityAction { get; set; }
    }
}