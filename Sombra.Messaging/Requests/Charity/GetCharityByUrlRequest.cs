using Sombra.Messaging.Responses.Charity;

namespace Sombra.Messaging.Requests.Charity
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetCharityByUrlRequest : Request<GetCharityByUrlResponse>
    {
        [CacheKey]
        public string Url { get; set; }
    }
}