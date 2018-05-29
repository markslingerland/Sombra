using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Responses
{
    public class GetCharityActionByKeyResponse : Response
    {
        public bool Success { get; set; }
        public CharityAction Content { get; set; }
    }
}