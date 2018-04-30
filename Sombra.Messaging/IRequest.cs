namespace Sombra.Messaging
{
    public interface IRequest<TResponse> : IMessage where TResponse : class, IResponse
    { }
}