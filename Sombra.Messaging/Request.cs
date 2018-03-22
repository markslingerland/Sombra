namespace Sombra.Messaging
{
    public class Request<TResponse> : Message, IRequest<TResponse>
        where TResponse : class, IResponse
    {
    }
}