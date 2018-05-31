using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.CharityAction
{
    public class ApproveCharityActionResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}