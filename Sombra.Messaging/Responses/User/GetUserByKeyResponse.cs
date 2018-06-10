namespace Sombra.Messaging.Responses.User
{
    public class GetUserByKeyResponse : Response
    {
        public bool UserExists { get; set; }
        public Shared.User User { get; set; }
    }
}