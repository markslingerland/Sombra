using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class GetRandomCharitiesRequest : Request<GetRandomCharitiesResponse>
    {
        public int Amount { get; set; }
    }
}