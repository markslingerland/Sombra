namespace Sombra.Messaging
{
    public interface IRequest<TResponse> where TResponse : class, IMessage
    {
    }
}