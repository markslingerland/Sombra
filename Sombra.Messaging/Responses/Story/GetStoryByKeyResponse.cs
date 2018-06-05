namespace Sombra.Messaging.Responses.Story
{
    public class GetStoryByKeyResponse : Response
    {
        public bool Success { get; set; }
        public Shared.Story Story { get; set; }
    }
}