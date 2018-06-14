using System.ComponentModel.DataAnnotations;

namespace Sombra.Web.ViewModels
{
    public class LoginViewModel{
        public Core.Enums.CredentialType CredentialType { get; set; }
        public string Identifier { get; set; }
        [DataType(DataType.Password)]
        public string Secret { get; set; }
    }
}