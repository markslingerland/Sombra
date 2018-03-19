using System.Threading.Tasks;

namespace Sombra.Messaging
{
    public interface IAsyncRequestHandler<in TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest message);
    }
}
