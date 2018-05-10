namespace Sombra.Messaging.DependencyValidation
{
    public class PingResponse<TResponse> : Response
        where TResponse : class, IResponse
    {
        public bool IsOnline { get; set; }
    }
}