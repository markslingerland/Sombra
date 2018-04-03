using System;
using System.Collections.Generic;

namespace Sombra.Web.Areas.Development.Models
{
    public class AuthenticationQuery
    {
        public string LoginTypeCode { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
    }
}