namespace Sombra.Messaging.Responses.Identity
{
    public class CreateIdentityResponse : CrudResponse
    {
        public string ActivationToken { get; set; }
    }
}