namespace Sombra.Messaging
{
    public abstract class Request<TResponse> : Message, IRequest<TResponse>
        where TResponse : class, IResponse
    {
    }
}