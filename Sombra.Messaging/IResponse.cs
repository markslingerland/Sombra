namespace Sombra.Messaging
{
    public interface IResponse : IMessage
    {
        bool Success { get; set; }
    }
}