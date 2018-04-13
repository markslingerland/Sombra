namespace Sombra.Messaging
{
    public abstract class Response : Message, IResponse
    {
        public bool Success { get; set; } = true;
    }
}