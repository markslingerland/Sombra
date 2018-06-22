using Sombra.Messaging.Responses.Donate;

namespace Sombra.Messaging.Requests.Donate
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetCharitiesRequest : Request<GetCharitiesResponse> { }
}