﻿using System.Threading.Tasks;

namespace Sombra.Messaging.Infrastructure
{
    public interface IAsyncRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class, IResponse
    {
        Task<TResponse> Handle(TRequest message);
    }
}
