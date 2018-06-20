using System.Threading.Tasks;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Infrastructure
{
    public abstract class AsyncCrudRequestHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : CrudResponse<TResponse>, new()
    {
        public abstract Task<TResponse> Handle(TRequest message);

        public TResponse Error(ErrorType errorType) => new TResponse {ErrorType = errorType};
    }
}
