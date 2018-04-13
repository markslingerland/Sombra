namespace Sombra.Messaging
{
    public abstract class Response : Message, IResponse
    {
        public bool IsRequestSuccessful { get; set; } = true;
    }
}