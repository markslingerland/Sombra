using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Requests.Template;

namespace Sombra.TemplateService.DAL
{
    public class EmailTemplateContext : SombraContext<EmailTemplateContext>
    {
        public EmailTemplateContext() { }

        public EmailTemplateContext(DbContextOptions<EmailTemplateContext> options, SombraContextOptions sombraContextOptions) : base(options, sombraContextOptions) { }

        protected override void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Template>().HasData(
                    new Template { Id = Guid.NewGuid(), TemplateKey = EmailType.ForgotPassword, Body = GetTemplateFromFile(EmailType.ForgotPassword) },
                    new Template { Id = Guid.NewGuid(), TemplateKey = EmailType.ConfirmAccount, Body = GetTemplateFromFile(EmailType.ConfirmAccount) }
                );
        }

        private static string GetTemplateFromFile(EmailType emailType)
        {
            return File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Seed\\{emailType.ToString()}.html");
        }

        public DbSet<Template> Templates { get; set; }
    }
}