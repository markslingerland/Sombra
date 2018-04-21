using System.Threading.Tasks;

namespace Sombra.Messaging.Infrastructure
{
    public interface IAutoResponderRequestDispatcher
    {
        Task<TResponse> DispatchAsync<TRequest, TResponse, THandler>(TRequest message)
            where TRequest : class, IRequest<TResponse>
            where THandler : IAsyncRequestHandler<TRequest, TResponse>
            where TResponse : class, IResponse;
    }
}