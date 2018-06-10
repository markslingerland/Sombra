using Sombra.Messaging.Responses.Charity;

namespace Sombra.Messaging.Requests.Charity
{
    [Cachable]
    public class GetCharityByUrlRequest : Request<GetCharityByUrlResponse>
    {
        [CacheKey]
        public string Url { get; set; }
    }
}