using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService.UnitTests
{
    [TestClass]
    public class GetSearchResultRequestHandlerTest
    {
        [TestMethod]
        public async Task GetSearchResultRequestHandler_Handle_Returns_Correct()
        {
            SearchContext.OpenInMemoryConnection();           

            try
            {
                GetSearchResultResponse response;
                var content = new Content(){
                    Category = Core.Enums.Category.MilieuEnNatuurbehoud | Core.Enums.Category.Gezondheid,
                    CharityKey = Guid.NewGuid(),
                    Image = "No image given",
                    Name = "TestName",
                    Description = "This is a very good testing slogan",
                    Type = Core.Enums.SearchContentType.Charity
                };   

                var content1 = new Content(){
                    Category = Core.Enums.Category.Gezondheid,
                    CharityKey = Guid.NewGuid(),
                    Image = "No image given",
                    Name = "TestName1",
                    Description = "This is a very good testing slogan",
                    Type = Core.Enums.SearchContentType.Charity
                };  

                var getSearchResultRequest = new GetSearchResultRequest(){
                    Categories = Core.Enums.Category.MilieuEnNatuurbehoud,
                    Keyword = "TestName",
                    SearchContentType = Core.Enums.SearchContentType.Charity,
                    PageNumber = 0,
                    PageSize = 1,
                    SortOrder = Core.Enums.SortOrder.Asc,
                };     

                using (var context = SearchContext.GetInMemoryContext())
                {
                    context.Content.Add(content);
                    context.Content.Add(content1);
                    context.SaveChanges();

                    var handler = new GetSearchResultRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(getSearchResultRequest);  
                }        

                using (var context = SearchContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, response.TotalResult);
                    Assert.IsTrue(response.IsRequestSuccessful);
                    Assert.AreEqual(content.Category, response.Results.Single().Category);
                    Assert.AreEqual(content.CharityKey, response.Results.Single().CharityKey);
                    Assert.AreEqual(content.Description, response.Results.Single().Description);
                    Assert.AreEqual(content.Image, response.Results.Single().Image);
                    Assert.AreEqual(content.Name, response.Results.Single().Name);
                    Assert.AreEqual(content.Type, response.Results.Single().Type);
                }
            }
            finally
            {
                SearchContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetSearchResultRequestHandler_Handle_Returns_Null()
        {
            SearchContext.OpenInMemoryConnection();           

            try
            {
                GetSearchResultResponse response;
                var content = new Content(){
                    Category = Core.Enums.Category.MilieuEnNatuurbehoud,
                    CharityKey = Guid.NewGuid(),
                    Image = "No image given",
                    Name = "TestName",
                    Description = "This is a very good testing slogan",
                    Type = Core.Enums.SearchContentType.Charity
                };   

                var content1 = new Content(){
                    Category = Core.Enums.Category.Gezondheid,
                    CharityKey = Guid.NewGuid(),
                    Image = "No image given",
                    Name = "TestName1",
                    Description = "This is a very good testing slogan",
                    Type = Core.Enums.SearchContentType.Charity
                };  

                var getSearchResultRequest = new GetSearchResultRequest(){
                    Categories = Core.Enums.Category.MilieuEnNatuurbehoud,
                    Keyword = "Null",
                    SearchContentType = Core.Enums.SearchContentType.Charity,
                    PageNumber = 0,
                    PageSize = 1,
                    SortOrder = Core.Enums.SortOrder.Asc,
                };     

                using (var context = SearchContext.GetInMemoryContext())
                {
                    context.Content.Add(content);
                    context.Content.Add(content1);
                    context.SaveChanges();

                    var handler = new GetSearchResultRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(getSearchResultRequest);  
                }        

                using (var context = SearchContext.GetInMemoryContext())
                {
                    Assert.AreEqual(0, response.TotalResult);
                    Assert.IsTrue(response.IsRequestSuccessful);
                }
            }
            finally
            {
                SearchContext.CloseInMemoryConnection();
            }
        }
        
    }
}