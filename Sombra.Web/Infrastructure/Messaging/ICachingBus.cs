using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Messaging;

namespace Sombra.Web.Infrastructure.Messaging
{
    public interface ICachingBus
    {
        IBus Bus { get; }
        Task<TResponse> RequestAsync<TResponse>(IRequest<TResponse> request, bool skipCache = false) where TResponse : class, IResponse;
        Task PublishAsync<T>(T message) where T : class;
    }
}