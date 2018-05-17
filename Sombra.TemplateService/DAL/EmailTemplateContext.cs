using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Requests;

namespace Sombra.TemplateService.DAL
{
    public class EmailTemplateContext : SombraContext<EmailTemplateContext>
    {
        public EmailTemplateContext() { }

        public EmailTemplateContext(DbContextOptions<EmailTemplateContext> options, bool seed = false) : base(options, seed) { }

        protected override void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemplateEntity>().HasData(
                    new TemplateEntity { Id = Guid.NewGuid(), TemplateId = EmailType.ForgotPassword, Template = GetTemplateFromFile(EmailType.ForgotPassword) },
                    new TemplateEntity { Id = Guid.NewGuid(), TemplateId = EmailType.ConfirmAccount, Template = GetTemplateFromFile(EmailType.ConfirmAccount) }
                );
        }

        private static string GetTemplateFromFile(EmailType emailType)
        {
            return File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Seed\\{emailType.ToString()}.html");
        }

        public DbSet<TemplateEntity> Template { get; set; }
    }
}