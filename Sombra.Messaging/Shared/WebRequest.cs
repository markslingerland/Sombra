using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Shared
{
    public class WebRequest : Message
    {
        public string Url { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public Dictionary<string, string> RouteValues { get; set; }
    }
}