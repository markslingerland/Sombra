using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Messaging;

namespace Sombra.Web.Infrastructure.Messaging
{
    public interface ICachingBus
    {
        IBus Bus { get; }
        Task<TResponse> RequestAsync<TResponse>(IRequest<TResponse> request, CachingOptions options = CachingOptions.None) where TResponse : class, IResponse;
        Task PublishAsync<TEvent>(TEvent message) where TEvent : class, IEvent;
    }
}