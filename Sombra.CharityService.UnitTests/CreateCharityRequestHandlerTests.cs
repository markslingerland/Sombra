﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.CharityService.DAL;
using System;
using Sombra.Core.Enums;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.Charity;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.CharityService.UnitTests
{
    [TestClass]
    public class CreateCharityRequestHandlerTests
    {
        [TestMethod]
        public async Task CreateCharityRequest_Handle_Returns_Success()
        {
            CharityContext.OpenInMemoryConnection();

            try
            {
                CreateCharityResponse response;
                var request = new CreateCharityRequest
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "testName",
                    OwnerUserName = "testOwnerUserName",
                    Email = "test@test.com",
                    Category = Category.None,
                    KVKNumber = "",
                    IBAN = "0-IBAN",
                    CoverImage = "",
                    Slogan = "Test"

                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new CreateCharityRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.AreEqual(request.CharityKey, context.Charities.Single().CharityKey);
                    Assert.AreEqual(request.Name, context.Charities.Single().Name);
                    Assert.AreEqual(request.OwnerUserName, context.Charities.Single().OwnerUserName);
                    Assert.AreEqual(request.Email, context.Charities.Single().Email);
                    Assert.AreEqual(request.Category, context.Charities.Single().Category);
                    Assert.AreEqual(request.KVKNumber, context.Charities.Single().KVKNumber);
                    Assert.AreEqual(request.IBAN, context.Charities.Single().IBAN);
                    Assert.AreEqual(request.CoverImage, context.Charities.Single().CoverImage);
                    Assert.AreEqual(request.Slogan, context.Charities.Single().Slogan);
                    Assert.IsFalse(context.Charities.Single().IsApproved);
                    Assert.IsTrue(response.IsSuccess);
                }
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task CreateCharityRequest_Handle_Returns_InvalidKey()
        {
            CharityContext.OpenInMemoryConnection();

            try
            {
                CreateCharityResponse response;
                var request = new CreateCharityRequest
                {
                    CharityKey = Guid.Empty,
                    Name = "testName",
                    OwnerUserName = "testOwnerUserName",
                    Email = "test@test.com",
                    Category = Category.None,
                    KVKNumber = "",
                    IBAN = "0-IBAN",
                    CoverImage = "",
                    Slogan = "Test"

                };

                using (var context = CharityContext.GetInMemoryContext())
                {
                    var handler = new CreateCharityRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(request);
                }

                using (var context = CharityContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.Charities.Any());
                    Assert.AreEqual(ErrorType.InvalidKey, response.ErrorType);
                    Assert.IsFalse(response.IsSuccess);
                }
            }
            finally
            {
                CharityContext.CloseInMemoryConnection();
            }
        }
    }
}