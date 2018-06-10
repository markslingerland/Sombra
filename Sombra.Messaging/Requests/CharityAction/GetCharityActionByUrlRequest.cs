using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.Messaging.Requests.CharityAction
{
    public class GetCharityActionByUrlRequest : Request<GetCharityActionByUrlResponse>
    {
        public string CharityUrl { get; set; }
        public string CharityActionUrlComponent { get; set; }
    }
}