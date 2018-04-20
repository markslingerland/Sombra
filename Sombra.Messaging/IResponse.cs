namespace Sombra.Messaging
{
    public interface IResponse : IMessage
    {
        bool IsRequestSuccessful { get; set; }
    }
}