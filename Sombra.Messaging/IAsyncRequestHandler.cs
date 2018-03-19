using System.Threading.Tasks;

namespace Sombra.Messaging
{
    public interface IAsyncRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest message);
    }
}
