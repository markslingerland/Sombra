using System;
using System.Collections.Generic;

namespace Sombra.Messaging.Responses{
    public class EmailTemplateResponse : Response {
        public string Template { get; set; }
        public bool HasTemplate => !string.IsNullOrEmpty(Template);
    }
}

