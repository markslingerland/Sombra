﻿using System.Threading.Tasks;
using EasyNetQ;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.DependencyValidation
{
    public class PingRequestHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : PingResponse, new()
    {
        public Task<TResponse> Handle(TRequest message)
        {
            return Task.FromResult(new TResponse
            {
                IsOnline = true
            });
        }
    }

    //public class TemplatePingRequestHandler : IAsyncRequestHandler<Ping<EmailTemplateRequest, EmailTemplateResponse>, PingResponse<EmailTemplateResponse>>
    //{
    //    public async Task<PingResponse<EmailTemplateResponse>> Handle(Ping<EmailTemplateRequest, EmailTemplateResponse> message)
    //    {
    //        return new PingResponse<EmailTemplateResponse>
    //        {
    //            IsOnline = true
    //        };
    //    }
    //}

    //public class abc
    //{
    //    public abc(IBus bus)
    //    {
    //        var ping = new Ping<EmailTemplateRequest, EmailTemplateResponse>();
    //        var response = bus.RequestAsync(ping);

    //        var handler = new PingRequestHandler<Ping<EmailTemplateRequest, EmailTemplateResponse>, PingResponse>();


    //        var result = handler.Handle(ping);
    //    }
    //}
}
