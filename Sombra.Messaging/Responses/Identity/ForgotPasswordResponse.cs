namespace Sombra.Messaging.Responses.Identity
{
    public class ForgotPasswordResponse : CrudResponse
    {
        public string Secret { get; set; }
    }
}