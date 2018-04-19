using System.ComponentModel.DataAnnotations;

namespace Sombra.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        public string VerifiedPassword { get; set; }
    }
}