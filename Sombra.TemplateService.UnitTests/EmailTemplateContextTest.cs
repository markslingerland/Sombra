using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.TemplateService.DAL;
using System.Threading.Tasks;
using Sombra.Messaging.Requests.Template;
using Sombra.Messaging.Responses.Template;

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
                    var template = new Template
                    {
                        TemplateKey = EmailType.ForgotPassword,
                        Body = "template [[test]]",

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
                    var template = new Template
                    {
                        TemplateKey = EmailType.ForgotPassword,
                        Body = null,

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
