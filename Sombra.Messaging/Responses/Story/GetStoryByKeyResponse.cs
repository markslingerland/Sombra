namespace Sombra.Messaging.Responses.Story
{
    public class GetStoryByKeyResponse : Response
    {
        public bool IsSuccess { get; set; }
        public Shared.Story Story { get; set; }
    }
}