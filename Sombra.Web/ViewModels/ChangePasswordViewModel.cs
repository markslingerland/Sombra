using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sombra.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "The password must atleast contain 5 or more characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        


        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "The password must atleast contain 5 or more characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Both passwords must be equal!")]
        public string VerifiedPassword { get; set; }
    }
}