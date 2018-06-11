namespace Sombra.Messaging.Responses.CharityAction
{
    public class GetCharityActionByKeyResponse : Response
    {
        public bool IsSuccess { get; set; }
        public Shared.CharityAction CharityAction { get; set; }
    }
}