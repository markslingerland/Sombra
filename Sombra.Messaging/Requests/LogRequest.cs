﻿using System;
using System.Collections.Generic;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests
{
    public class LogRequest : Message, IRequest<LogResponse>
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public IEnumerable<string> MessageTypes { get; set; }
    }
}