namespace Sombra.Web.ViewModels
{
    public class LoginViewModel{
        public Core.Enums.CredentialType CredentialType { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
    }
}