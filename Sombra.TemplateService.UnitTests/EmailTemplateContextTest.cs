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
            EmailTemplateContext.OpenInMemoryConnection();
            try
            {
                using (var context = EmailTemplateContext.GetInMemoryContext())
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
                var request = new EmailTemplateRequest(EmailType.ForgotPassword);


                EmailTemplateResponse response;

                //Act
                using (var context = EmailTemplateContext.GetInMemoryContext())
                {
                    var handler = new EmailTemplateRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                Assert.AreEqual(response.Template, "template [[test]]");
                Assert.IsTrue(response.HasTemplate);
            }
            finally
            {
                EmailTemplateContext.CloseInMemoryConnection();
            }
            
        }
        [TestMethod]
        public async Task EmailTemplateService_Handle_Returns_Null()
        {
            EmailTemplateContext.OpenInMemoryConnection();
            try
            {
                //Arrange
                using (var context = EmailTemplateContext.GetInMemoryContext())
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
                var request = new EmailTemplateRequest(EmailType.ForgotPassword);

                EmailTemplateResponse response;

                //Act
                using (var context = EmailTemplateContext.GetInMemoryContext())
                {
                    var handler = new EmailTemplateRequestHandler(context);
                    response = await handler.Handle(request);
                }

                //Assert
                Assert.IsFalse(response.HasTemplate);
            }
            finally
            {
                EmailTemplateContext.CloseInMemoryConnection();
            }

        }
    }
}
