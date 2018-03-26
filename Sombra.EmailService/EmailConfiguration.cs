using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.EmailService
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; } //= "smtp.gmail.com";
        public int SmtpPort { get; set; } //= 465;
        public string SmtpUsername { get; set; } //= "ikdoneernu@gmail.com";
        public string SmtpPassword { get; set; } //= "ikdoneernu@ikdoneer.nu";
        public string PopServer { get; set; }
        public int PopPort { get; set; }
        public string PopUsername { get; set; }
        public string PopPassword { get; set; }
    }
}
