using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Donate
{
    public class MakeDonationResponse : CrudResponse<MakeDonationResponse>
    {
        public string ThankYou { get; set; }
        public string CoverImage { get; set; }
    }
}