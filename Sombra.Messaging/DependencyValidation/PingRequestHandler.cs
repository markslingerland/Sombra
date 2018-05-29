using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;

namespace Sombra.Messaging.DependencyValidation
{
    public class PingRequestHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : PingResponse, new()
    {
        public Task<TResponse> Handle(TRequest message)
        {
            return Task.FromResult(new TResponse
            {
                IsOnline = true
            });
        }
    }
}