namespace Sombra.Web{
    public class LoginViewModel{
        public Core.Enums.CredentialType LoginTypeCode { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
    }
}