namespace Sombra.Messaging.Responses.Story
{
    public class GetStoryByUrlResponse : Response
    {
        public bool IsSuccess { get; set; }
        public Shared.Story Story { get; set; }
    }
}