using EasyNetQ.AutoSubscribe;

namespace Sombra.Messaging.Infrastructure
{
    public interface IAsyncMessageHandler<in TMessage> : IConsumeAsync<TMessage>
        where TMessage : class, IMessage
    {
    }
}