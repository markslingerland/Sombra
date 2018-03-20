﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Sombra.Messaging.Infrastructure
{
    public class AutoResponderMessageDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public AutoResponderMessageDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task DispatchAsync<TRequest, TResponse, THandler>(TRequest message)
            where TRequest : class, IRequest<TResponse>
            where THandler : IAsyncRequestHandler<TRequest, TResponse>
            where TResponse : class
        {
            var handler = _serviceProvider.GetService<THandler>();

            return handler.Handle(message);
        }
    }
}