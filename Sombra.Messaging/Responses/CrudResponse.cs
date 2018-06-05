using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses
{
    public abstract class CrudResponse : Response
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}