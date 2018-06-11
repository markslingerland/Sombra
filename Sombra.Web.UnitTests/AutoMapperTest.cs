using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sombra.Infrastructure;
using Sombra.Web.Infrastructure;

namespace Sombra.Web.UnitTests
{
    [TestClass]
    public class AutoMapperTest
    {
        [TestMethod]
        public void ValidateMapper()
        {
            AutoMapperHelper.BuildMapper(new MappingProfile());
        }
    }
}