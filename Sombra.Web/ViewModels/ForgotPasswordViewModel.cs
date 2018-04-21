using System.ComponentModel.DataAnnotations;

namespace Sombra.Web.ViewModels{
    public class ForgotPasswordViewModel{
        [Display(Name = "e-mailadres")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAdress { get; set; }

    }
}