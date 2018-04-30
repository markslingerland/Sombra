using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Requests;

namespace Sombra.TemplateService.DAL
{
    public class EmailTemplateContext : SombraContext
    {
        public EmailTemplateContext(DbContextOptions<EmailTemplateContext> options) : base(options)
        {
        }

        public EmailTemplateContext(DbContextOptions<EmailTemplateContext> options, bool seed) : base(options, seed)
        {
        }

        protected override void Seed(ModelBuilder modelBuilder)
        {
            var templateDirectory = $"{Directory.GetCurrentDirectory()}\\Seed";
            var template = File.ReadAllText($"{templateDirectory}\\{EmailType.ForgotPassword.ToString()}.html");


            modelBuilder.Entity<TemplateEntity>().HasData(
                    new TemplateEntity {Id = Guid.NewGuid(), TemplateId = EmailType.ForgotPassword, Template = template}
                );
        }

        public DbSet<TemplateEntity> Template { get; set; }
    }
}