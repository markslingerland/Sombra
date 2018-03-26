using System;
using System.Collections.Generic;

namespace Sombra.Web.Areas.Development.Models
{
    public class LogsQuery
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public IEnumerable<string> MessageTypes { get; set; }
    }
}