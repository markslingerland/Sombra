using AutoMapper;

namespace Sombra.Messaging
{
    public abstract class Response : Message, IResponse
    {
        [IgnoreMap]
        public bool IsRequestSuccessful { get; set; } = true;
    }
}