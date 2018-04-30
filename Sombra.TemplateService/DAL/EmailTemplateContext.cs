using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.TemplateService.Templates.DAL
{
    public class EmailTemplateContext : SombraContext
    {
        public EmailTemplateContext(DbContextOptions<EmailTemplateContext> options) : base(options)
        {
        }

        public EmailTemplateContext(DbContextOptions<EmailTemplateContext> options, bool seed) : base(options, seed)
        {
        }

        public DbSet<TemplateEntity> Template { get; set; }
    }
}
