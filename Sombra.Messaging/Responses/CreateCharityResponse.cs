namespace Sombra.Messaging.Responses
{
    public class CreateCharityResponse : Response
    {
        public CreateCharityResponse()
        {
        }

        public CreateCharityResponse(bool success)
        {
            Success = success;
        }

        public bool Success { get; set; }
    }
}