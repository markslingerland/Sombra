namespace Sombra.Messaging.Responses
{
    public class CreateUserResponse : Response
    {
        public bool Success { get; set; }
        public CreateUserErrorType ErrorType { get; set; }
    }

    public enum CreateUserErrorType
    {
        Other = 0,
        EmailExists = 1
    }
}