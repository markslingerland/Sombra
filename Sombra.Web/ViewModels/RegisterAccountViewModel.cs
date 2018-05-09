using System.ComponentModel.DataAnnotations;

namespace Sombra.Web.ViewModels
{
    public class RegisterAccountViewModel
    {
        [Required(ErrorMessage = "De voornaam is verplicht!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "De achternaam is verplicht!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Een e-mailadres is verplicht!")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Een wachtwoord is verplicht!")]
        [StringLength(255, ErrorMessage = "Het wachtwoord moet minimaal 5 tekens lang zijn!", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bevestig je wachtwoord")]
        [StringLength(255, ErrorMessage = "Het wachtwoord moet minimaal 5 tekens lang zijn!", MinimumLength = 5)]
        [Compare("Password", ErrorMessage = "De ingegeven wachtwoorden moeten overeenkomen!")]
        public string PasswordVerification { get; set; }
    }
}