using System.ComponentModel.DataAnnotations;

namespace Sombra.Web.ViewModels{
    public class ForgotPasswordViewModel{
        [Display(Name = "e-mailadres")]
        [Required()]
        [EmailAddress()]
        public string EmailAddress { get; set; }

    }
}