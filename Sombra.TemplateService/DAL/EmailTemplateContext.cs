using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Sombra.TemplateService.Templates.DAL
{
    public class EmailTemplateContext : DbContext
    {
        public EmailTemplateContext(DbContextOptions<EmailTemplateContext> options) : base(options)
        {
        }
        public DbSet<TemplateEntity> Template { get; set; }
    }
}
