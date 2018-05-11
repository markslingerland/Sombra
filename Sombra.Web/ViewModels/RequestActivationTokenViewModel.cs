using System.ComponentModel.DataAnnotations;

namespace Sombra.Web.ViewModels
{
    public class RequestActivationTokenViewModel
    {
        [Required(ErrorMessage = "Een e-mailadres is verplicht!")]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}