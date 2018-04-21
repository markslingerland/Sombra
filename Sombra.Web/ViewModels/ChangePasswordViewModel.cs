using System.ComponentModel.DataAnnotations;

namespace Sombra.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required()]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required()]
        [DataType(DataType.Password)]
        public string VerifiedPassword { get; set; }
    }
}