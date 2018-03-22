namespace Sombra.Messaging
{
    public interface IRequest<TResponse> : IRequest where TResponse : class, IMessage
    { }

    public interface IRequest
    { }
}