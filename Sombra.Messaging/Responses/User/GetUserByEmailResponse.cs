namespace Sombra.Messaging.Responses.User
{
    public class GetUserByEmailResponse : Response
    {
        public bool UserExists { get; set; }
        public Shared.User User { get; set; }
    }
}