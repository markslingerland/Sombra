using EasyNetQ.AutoSubscribe;

namespace Sombra.Messaging.Infrastructure
{
    public interface IAsyncEventHandler<in TMessage> : IConsumeAsync<TMessage>
        where TMessage : class, IEvent
    {
    }
}