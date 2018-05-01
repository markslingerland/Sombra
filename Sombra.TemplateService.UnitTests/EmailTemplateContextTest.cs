using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.TemplateService.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sombra.TemplateService.UnitTests
{
    [TestClass]
    public class EmailTemplateContextTest
    {
        [TestMethod]
        public async Task EmailTemplateService_Handle_Returns_Template()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                //Arrange
                var options = new DbContextOptionsBuilder<EmailTemplateContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new EmailTemplateContext(options, false))
                {
                    context.Database.EnsureCreated();

                    var template = new TemplateEntity
                    {
                        TemplateId = EmailType.ForgotPassword,
                        Template = "template [[test]]",

                    };

                    context.Add(template);
                    context.SaveChanges();

                }
                var request = new EmailTemplateRequest(EmailType.ForgotPassword, new Dictionary<string, string>() { { "test", "test" } } );


                EmailTemplateResponse response;

                //Act
                using (var context = new EmailTemplateContext(options, false))
                {
                    var handler = new EmailTemplateRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = new EmailTemplateContext(options, false))
                {
                    Assert.AreEqual(response.Template, "template test");
                    Assert.IsTrue(response.HasTemplate);
                }
            }
            finally
            {
                connection.Close();
            }
            
        }
        [TestMethod]
        public async Task EmailTemplateService_Handle_Returns_Null()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                //Arrange
                var options = new DbContextOptionsBuilder<EmailTemplateContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new EmailTemplateContext(options, false))
                {
                    context.Database.EnsureCreated();

                    var template = new TemplateEntity
                    {
                        TemplateId = EmailType.ForgotPassword,
                        Template = null,

                    };

                    context.Add(template);
                    context.SaveChanges();

                }
                var request = new EmailTemplateRequest(EmailType.ForgotPassword, new Dictionary<string, string>() { { "test", "test" } } );

                EmailTemplateResponse response;

                //Act
                using (var context = new EmailTemplateContext(options, false))
                {
                    var handler = new EmailTemplateRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                using (var context = new EmailTemplateContext(options, false))
                {
                    Assert.IsFalse(response.HasTemplate);
                }
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
