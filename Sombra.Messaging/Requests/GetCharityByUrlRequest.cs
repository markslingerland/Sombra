using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class GetCharityByUrlRequest : Request<GetCharityByUrlResponse>
    {
        public string Url { get; set; }
    }
}