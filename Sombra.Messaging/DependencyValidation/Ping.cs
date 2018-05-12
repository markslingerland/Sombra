namespace Sombra.Messaging.DependencyValidation
{
    public class Ping<TRequest, TResponse> : Request<PingResponse<TResponse>>
        where TRequest : class, IRequest<TResponse>
        where TResponse : class, IResponse
    {
    }
}