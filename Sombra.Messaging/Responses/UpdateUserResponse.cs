namespace Sombra.Messaging.Responses
{
    public class UpdateUserResponse : Response
    {
        public bool Success { get; set; }
        public UpdateUserErrorType ErrorType { get; set; }
    }

    public enum UpdateUserErrorType
    {
        Other = 0,
        NotFound = 1,
        EmailExists = 2
    }
}