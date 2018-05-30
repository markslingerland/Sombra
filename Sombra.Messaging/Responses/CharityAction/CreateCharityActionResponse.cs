using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.CharityAction
{
    public class CreateCharityActionResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}