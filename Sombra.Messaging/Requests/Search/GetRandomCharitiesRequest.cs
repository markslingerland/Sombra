using Sombra.Messaging.Responses.Search;

namespace Sombra.Messaging.Requests.Search
{
    public class GetRandomCharitiesRequest : Request<GetRandomCharitiesResponse>
    {
        public int Amount { get; set; }
    }
}