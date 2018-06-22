using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses
{
    public abstract class CrudResponse<T> : Response
        where T : CrudResponse<T>, new()
    {
        public bool IsSuccess { get; set; }
        public ErrorType ErrorType { get; set; }

        public static T Error(ErrorType errorType) => new T {ErrorType = errorType};
        public static T Success() => new T {IsSuccess = true};
    }
}