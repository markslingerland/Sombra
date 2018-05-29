using Sombra.Messaging.Responses.Charity;

namespace Sombra.Messaging.Requests.Charity
{
    public class GetCharityByUrlRequest : Request<GetCharityByUrlResponse>
    {
        public string Url { get; set; }
    }
}