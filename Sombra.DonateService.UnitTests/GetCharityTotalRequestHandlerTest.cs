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
    public class GetCharityTotalRequestHandlerTest
    {
        [TestMethod]
        public async Task GetCharityTotalRequestHandler_Handle_With_CharityActions_Return_Correct()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                GetCharityTotalResponse response;
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
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity
                };

                var charityDonation = new CharityDonation
                {
                    Amount = 1m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-1),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityDonation1 = new CharityDonation
                {
                    Amount = 5m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-4),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityActionDonation = new CharityActionDonation
                {
                    Amount = 10m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-3),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    CharityAction = charityAction
                };

                var getCharityTotalRequest = new GetCharityTotalRequest
                {
                    CharityKey = charity.CharityKey,
                    From = DateTime.UtcNow.AddDays(-1),
                    To = DateTime.UtcNow,
                    IncludeCharityActions = true,
                    NumberOfDonations = 3,
                    SortOrder = SortOrder.Desc
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.CharityDonations.Add(charityDonation);
                    context.CharityDonations.Add(charityDonation1);
                    context.CharityActionDonations.Add(charityActionDonation);

                    context.SaveChanges();

                    var handler =
                        new GetCharityTotalRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(getCharityTotalRequest);
                }

                Assert.AreEqual(3, response.NumberOfDonators);
                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual(charityDonation.Amount, response.Donations.First().Amount);
                Assert.AreEqual(charityDonation.DateTimeStamp, response.Donations.First().DateTimeStamp);
                Assert.AreEqual(charityDonation.User.ProfileImage, response.Donations.First().ProfileImage);
                Assert.AreEqual(charityDonation.User.UserName, response.Donations.First().UserName);
                Assert.AreEqual(charityDonation1.Amount, response.Donations.Last().Amount);
                Assert.AreEqual(charityDonation1.DateTimeStamp, response.Donations.Last().DateTimeStamp);
                Assert.AreEqual(charityDonation1.User.ProfileImage, response.Donations.Last().ProfileImage);
                Assert.AreEqual(charityDonation1.User.UserName, response.Donations.Last().UserName);
                Assert.AreEqual(charityActionDonation.Amount + charityDonation.Amount + charityDonation1.Amount,
                    response.TotalDonatedAmount);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityTotalRequestHandler_Handle_Without_CharityActions_Return_Correct()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                GetCharityTotalResponse response;
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
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity
                };

                var charityDonation = new CharityDonation
                {
                    Amount = 1m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-1),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityDonation1 = new CharityDonation
                {
                    Amount = 5m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-4),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityActionDonation = new CharityActionDonation
                {
                    Amount = 10m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-3),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    CharityAction = charityAction
                };

                var getCharityTotalRequest = new GetCharityTotalRequest
                {
                    CharityKey = charity.CharityKey,
                    From = DateTime.UtcNow.AddDays(-1),
                    To = DateTime.UtcNow,
                    IncludeCharityActions = false,
                    NumberOfDonations = 3,
                    SortOrder = SortOrder.Desc
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.CharityDonations.Add(charityDonation);
                    context.CharityDonations.Add(charityDonation1);
                    context.CharityActionDonations.Add(charityActionDonation);

                    context.SaveChanges();

                    var handler =
                        new GetCharityTotalRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(getCharityTotalRequest);
                }

                Assert.AreEqual(2, response.NumberOfDonators);
                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual(charityDonation.Amount, response.Donations.First().Amount);
                Assert.AreEqual(charityDonation.DateTimeStamp, response.Donations.First().DateTimeStamp);
                Assert.AreEqual(charityDonation.User.ProfileImage, response.Donations.First().ProfileImage);
                Assert.AreEqual(charityDonation.User.UserName, response.Donations.First().UserName);
                Assert.AreEqual(charityDonation1.Amount, response.Donations.Last().Amount);
                Assert.AreEqual(charityDonation1.DateTimeStamp, response.Donations.Last().DateTimeStamp);
                Assert.AreEqual(charityDonation1.User.ProfileImage, response.Donations.Last().ProfileImage);
                Assert.AreEqual(charityDonation1.User.UserName, response.Donations.Last().UserName);
                Assert.AreEqual(charityDonation.Amount + charityDonation1.Amount, response.TotalDonatedAmount);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityTotalRequestHandler_Handle_With_Anonymous_Return_Correct()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                GetCharityTotalResponse response;
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
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity
                };

                var charityDonation = new CharityDonation
                {
                    Amount = 1m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-1),
                    DonationType = DonationType.Once,
                    IsAnonymous = true,
                    Charity = charity
                };

                var charityDonation1 = new CharityDonation
                {
                    Amount = 5m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-4),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityActionDonation = new CharityActionDonation
                {
                    Amount = 10m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-3),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    CharityAction = charityAction
                };

                var getCharityTotalRequest = new GetCharityTotalRequest
                {
                    CharityKey = charity.CharityKey,
                    From = DateTime.UtcNow.AddDays(-1),
                    To = DateTime.UtcNow,
                    IncludeCharityActions = false,
                    NumberOfDonations = 3,
                    SortOrder = SortOrder.Desc
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.CharityDonations.Add(charityDonation);
                    context.CharityDonations.Add(charityDonation1);
                    context.CharityActionDonations.Add(charityActionDonation);

                    context.SaveChanges();

                    var handler =
                        new GetCharityTotalRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(getCharityTotalRequest);
                }

                Assert.AreEqual(2, response.NumberOfDonators);
                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual(charityDonation.Amount, response.Donations.First().Amount);
                Assert.AreEqual(charityDonation.DateTimeStamp, response.Donations.First().DateTimeStamp);
                Assert.IsNull(response.Donations.First().ProfileImage);
                Assert.IsNull(response.Donations.First().UserName);
                Assert.AreEqual(charityDonation1.Amount, response.Donations.Last().Amount);
                Assert.AreEqual(charityDonation1.DateTimeStamp, response.Donations.Last().DateTimeStamp);
                Assert.AreEqual(charityDonation1.User.ProfileImage, response.Donations.Last().ProfileImage);
                Assert.AreEqual(charityDonation1.User.UserName, response.Donations.Last().UserName);
                Assert.AreEqual(charityDonation.Amount + charityDonation1.Amount, response.TotalDonatedAmount);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityTotalRequestHandler_Handle_SortOrder_Asc_Return_Correct()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                GetCharityTotalResponse response;
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
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity
                };

                var charityDonation = new CharityDonation
                {
                    Amount = 1m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-1),
                    DonationType = DonationType.Once,
                    IsAnonymous = true,
                    Charity = charity
                };

                var charityDonation1 = new CharityDonation
                {
                    Amount = 5m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-4),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityActionDonation = new CharityActionDonation
                {
                    Amount = 10m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-3),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    CharityAction = charityAction
                };

                var getCharityTotalRequest = new GetCharityTotalRequest
                {
                    CharityKey = charity.CharityKey,
                    From = DateTime.UtcNow.AddDays(-1),
                    To = DateTime.UtcNow,
                    IncludeCharityActions = false,
                    NumberOfDonations = 3,
                    SortOrder = SortOrder.Asc
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.CharityDonations.Add(charityDonation);
                    context.CharityDonations.Add(charityDonation1);
                    context.CharityActionDonations.Add(charityActionDonation);

                    context.SaveChanges();

                    var handler =
                        new GetCharityTotalRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(getCharityTotalRequest);
                }

                Assert.AreEqual(2, response.NumberOfDonators);
                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual(charityDonation.Amount, response.Donations.Last().Amount);
                Assert.AreEqual(charityDonation.DateTimeStamp, response.Donations.Last().DateTimeStamp);
                Assert.IsNull(response.Donations.Last().ProfileImage);
                Assert.IsNull(response.Donations.Last().UserName);
                Assert.AreEqual(charityDonation1.Amount, response.Donations.First().Amount);
                Assert.AreEqual(charityDonation1.DateTimeStamp, response.Donations.First().DateTimeStamp);
                Assert.AreEqual(charityDonation1.User.ProfileImage, response.Donations.First().ProfileImage);
                Assert.AreEqual(charityDonation1.User.UserName, response.Donations.First().UserName);
                Assert.AreEqual(charityDonation.Amount + charityDonation1.Amount, response.TotalDonatedAmount);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityTotalRequestHandler_Handle_With_Future_Donation_Return_Correct()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                GetCharityTotalResponse response;
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
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity
                };

                var charityDonationFuture = new CharityDonation
                {
                    Amount = 1m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(+1),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityDonation = new CharityDonation
                {
                    Amount = 1m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-1),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityDonation1 = new CharityDonation
                {
                    Amount = 5m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-4),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityActionDonation = new CharityActionDonation
                {
                    Amount = 10m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-3),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    CharityAction = charityAction
                };

                var getCharityTotalRequest = new GetCharityTotalRequest
                {
                    CharityKey = charity.CharityKey,
                    From = DateTime.UtcNow.AddDays(-1),
                    To = DateTime.UtcNow,
                    IncludeCharityActions = false,
                    NumberOfDonations = 3,
                    SortOrder = SortOrder.Desc
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.CharityDonations.Add(charityDonationFuture);
                    context.CharityDonations.Add(charityDonation);
                    context.CharityDonations.Add(charityDonation1);
                    context.CharityActionDonations.Add(charityActionDonation);

                    context.SaveChanges();

                    var handler =
                        new GetCharityTotalRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(getCharityTotalRequest);
                }

                Assert.AreEqual(2, response.NumberOfDonators);
                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual(charityDonation.Amount, response.Donations.First().Amount);
                Assert.AreEqual(charityDonation.DateTimeStamp, response.Donations.First().DateTimeStamp);
                Assert.AreEqual(charityDonation.User.ProfileImage, response.Donations.First().ProfileImage);
                Assert.AreEqual(charityDonation.User.UserName, response.Donations.First().UserName);
                Assert.AreEqual(charityDonation1.Amount, response.Donations.Last().Amount);
                Assert.AreEqual(charityDonation1.DateTimeStamp, response.Donations.Last().DateTimeStamp);
                Assert.AreEqual(charityDonation1.User.ProfileImage, response.Donations.Last().ProfileImage);
                Assert.AreEqual(charityDonation1.User.UserName, response.Donations.Last().UserName);
                Assert.AreEqual(charityDonation.Amount + charityDonation1.Amount, response.TotalDonatedAmount);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityTotalRequestHandler_Handle_Without_To_And_From_Return_Correct()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                GetCharityTotalResponse response;
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
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity
                };

                var charityDonation = new CharityDonation
                {
                    Amount = 1m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-1),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityDonation1 = new CharityDonation
                {
                    Amount = 5m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-4),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityActionDonation = new CharityActionDonation
                {
                    Amount = 10m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-3),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    CharityAction = charityAction
                };

                var getCharityTotalRequest = new GetCharityTotalRequest
                {
                    CharityKey = charity.CharityKey,
                    IncludeCharityActions = false,
                    NumberOfDonations = 3,
                    SortOrder = SortOrder.Desc
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    context.CharityDonations.Add(charityDonation);
                    context.CharityDonations.Add(charityDonation1);
                    context.CharityActionDonations.Add(charityActionDonation);

                    context.SaveChanges();

                    var handler =
                        new GetCharityTotalRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(getCharityTotalRequest);
                }

                Assert.AreEqual(2, response.NumberOfDonators);
                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual(charityDonation.Amount, response.Donations.First().Amount);
                Assert.AreEqual(charityDonation.DateTimeStamp, response.Donations.First().DateTimeStamp);
                Assert.AreEqual(charityDonation.User.ProfileImage, response.Donations.First().ProfileImage);
                Assert.AreEqual(charityDonation.User.UserName, response.Donations.First().UserName);
                Assert.AreEqual(charityDonation1.Amount, response.Donations.Last().Amount);
                Assert.AreEqual(charityDonation1.DateTimeStamp, response.Donations.Last().DateTimeStamp);
                Assert.AreEqual(charityDonation1.User.ProfileImage, response.Donations.Last().ProfileImage);
                Assert.AreEqual(charityDonation1.User.UserName, response.Donations.Last().UserName);
                Assert.AreEqual(charityDonation.Amount + charityDonation1.Amount, response.TotalDonatedAmount);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }

        [TestMethod]
        public async Task GetCharityTotalRequestHandler_Handle_With_CharityActions_Return_NoDonationsFound()
        {
            DonationsContext.OpenInMemoryConnection();

            try
            {
                GetCharityTotalResponse response;
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
                };

                var charityAction = new CharityAction()
                {
                    ActionEndDateTime = DateTime.UtcNow,
                    Name = "TestName",
                    CharityActionKey = Guid.NewGuid(),
                    Charity = charity
                };

                var charityDonation = new CharityDonation
                {
                    Amount = 1m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-1),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityDonation1 = new CharityDonation
                {
                    Amount = 5m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-4),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    Charity = charity,
                    User = user
                };

                var charityActionDonation = new CharityActionDonation
                {
                    Amount = 10m,
                    DateTimeStamp = DateTime.UtcNow.AddHours(-3),
                    DonationType = DonationType.Once,
                    IsAnonymous = false,
                    CharityAction = charityAction
                };

                var getCharityTotalRequest = new GetCharityTotalRequest
                {
                    CharityKey = charity.CharityKey,
                    From = DateTime.UtcNow.AddDays(-1),
                    To = DateTime.UtcNow,
                    IncludeCharityActions = true,
                    NumberOfDonations = 3,
                    SortOrder = SortOrder.Desc
                };

                using (var context = DonationsContext.GetInMemoryContext())
                {
                    var handler =
                        new GetCharityTotalRequestHandler(context, AutoMapperHelper.BuildMapper(new MappingProfile()));
                    response = await handler.Handle(getCharityTotalRequest);
                }

                Assert.AreEqual(ErrorType.NoDonationsFound, response.ErrorType);
                Assert.IsFalse(response.IsSuccess);
            }
            finally
            {
                DonationsContext.CloseInMemoryConnection();
            }
        }
    }
}