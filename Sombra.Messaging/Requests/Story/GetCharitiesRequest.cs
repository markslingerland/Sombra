using Sombra.Messaging.Responses.Story;

namespace Sombra.Messaging.Requests.Story
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetCharitiesRequest : Request<GetCharitiesResponse> { }
}