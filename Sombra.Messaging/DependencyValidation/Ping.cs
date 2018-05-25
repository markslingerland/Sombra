namespace Sombra.Messaging.DependencyValidation
{
    public class Ping<TRequest, TResponse> : Request<PingResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : class, IResponse
    {
    }
}