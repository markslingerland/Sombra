using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sombra.Web.UnitTests
{
    [TestClass]
    public class TestUnitTest
    {
        [TestMethod]
        public void This_Test_Will_Succeed()
        {
            var test = new TestClass();
            Assert.IsTrue(test.IsTrue);
        }
    }
}