using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses
{
    public abstract class CrudResponse<T> : Response
        where T : CrudResponse<T>, new()
    {
        public bool IsSuccess { get; set; }
        public ErrorType ErrorType { get; set; }

        public static T Success() => new T {IsSuccess = true};
    }
}