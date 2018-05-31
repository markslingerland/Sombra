using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.CharityAction
{
    public class UpdateCharityActionResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}