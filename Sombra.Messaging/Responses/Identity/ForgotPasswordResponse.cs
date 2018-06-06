namespace Sombra.Messaging.Responses.Identity
{
    public class ForgotPasswordResponse : CrudResponse<ForgotPasswordResponse>
    {
        public string Secret { get; set; }
    }
}