using System;
using System.Collections.Generic;

namespace Sombra.Web.Areas.Development.Models
{
    public class AuthenticationQuery
    {
        public Core.Enums.CredentialType LoginTypeCode { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
    }
}