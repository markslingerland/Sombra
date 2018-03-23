using System;

namespace Sombra.Web.Areas.Development.Models
{
    public class LogViewModel
    {
        public string Message { get; set; }
        public string MessageType { get; set; }
        public DateTime MessageReceived { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}