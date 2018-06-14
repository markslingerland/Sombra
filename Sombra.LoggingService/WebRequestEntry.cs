using System;
using System.Linq;
using Sombra.Infrastructure.DAL.Mongo;
using Sombra.Messaging.Shared;

namespace Sombra.LoggingService
{
    public class WebRequestEntry : DocumentEntity
    {
        public WebRequestEntry(WebRequest webRequest)
        {
            Url = webRequest.Url;
            DateTimeStamp = webRequest.DateTimeStamp;
            RouteValues = string.Join(", ", webRequest.RouteValues.Select(kv => $"{kv.Key}: {kv.Value}"));
        }

        public string Url { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public string RouteValues { get; set; }
    }
}