using System.Threading.Tasks;

namespace Sombra.Messaging
{
    public interface IAsyncRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        Task<TResponse> Handle(TRequest message);
    }
}
