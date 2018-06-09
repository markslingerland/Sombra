using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Core.Enums;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure;
using Sombra.Messaging.Requests.Donate;
using Sombra.Messaging.Responses.Donate;

namespace Sombra.DonateService.UnitTests
{
    [TestClass]
    public class MakeDonationRequestHandlerTest
    {
        [TestMethod]
        public async Task MakeDonationHandler_Handle_Return_Correct()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                MakeDonationResponse response;
                var user = new User
                {
                    UserKey = Guid.NewGuid(),
                    ProfileImage = "PrettyImage",
                    UserName = "Test Test"
                };

                var charity = new Charity()
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "TestName",
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity,
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var makeDonationRequest = new MakeDonationRequest
                {
                    CharityKey = charity.CharityKey,
                    Amount = 10m,
                    UserKey = user.UserKey,
                    IsAnonymous = false
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.Users.Add(user);
                    context.Charities.Add(charity);
                    await context.SaveChangesAsync();
                    var handler =
                        new MakeDonationRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(makeDonationRequest);
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.CharityDonations.Count());
                    Assert.IsTrue(response.IsSuccess);
                    Assert.AreEqual(makeDonationRequest.Amount, context.CharityDonations.First().Amount);
                    Assert.AreEqual(makeDonationRequest.IsAnonymous, context.CharityDonations.First().IsAnonymous);
                    Assert.AreEqual(charity.ThankYou, response.ThankYou);
                    Assert.AreEqual(charity.CoverImage, response.CoverImage);
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task MakeDonationHandler_Handle_CharityActionDonation_Return_Correct()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                MakeDonationResponse response;
                var user = new User
                {
                    UserKey = Guid.NewGuid(),
                    ProfileImage = "PrettyImage",
                    UserName = "Test Test"
                };

                var charity = new Charity()
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "TestName",
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity,
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var makeDonationRequest = new MakeDonationRequest
                {
                    CharityKey = charity.CharityKey,
                    CharityActionKey = charityAction.CharityActionKey,
                    Amount = 10m,
                    UserKey = user.UserKey,
                    IsAnonymous = false
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.Users.Add(user);
                    context.Charities.Add(charity);
                    context.CharityActions.Add(charityAction);
                    await context.SaveChangesAsync();
                    var handler =
                        new MakeDonationRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(makeDonationRequest);
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.CharityActionDonations.Count());
                    Assert.IsTrue(response.IsSuccess);
                    Assert.AreEqual(makeDonationRequest.Amount, context.CharityActionDonations.First().Amount);
                    Assert.AreEqual(makeDonationRequest.IsAnonymous,
                        context.CharityActionDonations.First().IsAnonymous);
                    Assert.AreEqual(charityAction.ThankYou, response.ThankYou);
                    Assert.AreEqual(charityAction.CoverImage, response.CoverImage);
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        public async Task MakeDonationHandler_Handle_CharityActionDonation_AsAnonymous_Return_Correct()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                MakeDonationResponse response;
                var charity = new Charity()
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "TestName",
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity,
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var makeDonationRequest = new MakeDonationRequest
                {
                    CharityKey = charity.CharityKey,
                    CharityActionKey = charityAction.CharityActionKey,
                    Amount = 10m,
                    IsAnonymous = false
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                    context.CharityActions.Add(charityAction);
                    await context.SaveChangesAsync();
                    var handler =
                        new MakeDonationRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(makeDonationRequest);
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.AreEqual(1, context.CharityActionDonations.Count());
                    Assert.IsTrue(response.IsSuccess);
                    Assert.AreEqual(makeDonationRequest.Amount, context.CharityActionDonations.First().Amount);
                    Assert.AreEqual(makeDonationRequest.IsAnonymous,
                        context.CharityActionDonations.First().IsAnonymous);
                    Assert.AreEqual(charityAction.ThankYou, response.ThankYou);
                    Assert.AreEqual(charityAction.CoverImage, response.CoverImage);
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task MakeDonationHandler_Handle_Return_UserNotFound()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                MakeDonationResponse response;
                var user = new User
                {
                    UserKey = Guid.NewGuid(),
                    ProfileImage = "PrettyImage",
                    UserName = "Test Test"
                };

                var charity = new Charity()
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "TestName",
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity,
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var makeDonationRequest = new MakeDonationRequest
                {
                    CharityKey = charity.CharityKey,
                    CharityActionKey = charityAction.CharityActionKey,
                    Amount = 10m,
                    UserKey = user.UserKey,
                    IsAnonymous = false
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                    context.CharityActions.Add(charityAction);
                    await context.SaveChangesAsync();
                    var handler =
                        new MakeDonationRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(makeDonationRequest);
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.CharityDonations.Any());
                    Assert.IsFalse(context.CharityActionDonations.Any());
                    Assert.AreEqual(ErrorType.UserNotFound, response.ErrorType);
                    Assert.IsFalse(response.IsSuccess);
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task MakeDonationHandler_Handle_Return_CharityNotFound()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                MakeDonationResponse response;
                var user = new User
                {
                    UserKey = Guid.NewGuid(),
                    ProfileImage = "PrettyImage",
                    UserName = "Test Test"
                };

                var charity = new Charity()
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "TestName",
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity,
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var makeDonationRequest = new MakeDonationRequest
                {
                    CharityKey = charity.CharityKey,
                    Amount = 10m,
                    UserKey = user.UserKey,
                    IsAnonymous = false
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    var handler =
                        new MakeDonationRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(makeDonationRequest);
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.CharityDonations.Any());
                    Assert.IsFalse(context.CharityActionDonations.Any());
                    Assert.AreEqual(ErrorType.CharityNotFound, response.ErrorType);
                    Assert.IsFalse(response.IsSuccess);
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task MakeDonationHandler_Handle_Return_CharityActionNotFound()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                MakeDonationResponse response;
                var user = new User
                {
                    UserKey = Guid.NewGuid(),
                    ProfileImage = "PrettyImage",
                    UserName = "Test Test"
                };

                var charity = new Charity()
                {
                    CharityKey = Guid.NewGuid(),
                    Name = "TestName",
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity,
                    CoverImage = "TestImage",
                    ThankYou = "ThankYou"
                };

                var makeDonationRequest = new MakeDonationRequest
                {
                    CharityKey = charity.CharityKey,
                    CharityActionKey = charityAction.CharityActionKey,
                    Amount = 10m,
                    UserKey = user.UserKey,
                    IsAnonymous = false
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.Charities.Add(charity);
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    var handler =
                        new MakeDonationRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(makeDonationRequest);
                }

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    Assert.IsFalse(context.CharityDonations.Any());
                    Assert.IsFalse(context.CharityActionDonations.Any());
                    Assert.AreEqual(ErrorType.CharityActionNotFound, response.ErrorType);
                    Assert.IsFalse(response.IsSuccess);
                }
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }
    }
}