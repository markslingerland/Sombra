namespace Sombra.Messaging.Responses.Identity
{
    public class CreateIdentityResponse : CrudResponse<CreateIdentityResponse>
    {
        public string ActivationToken { get; set; }
    }
}