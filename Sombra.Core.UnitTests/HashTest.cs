using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sombra.Core.UnitTests
{
    [TestClass]
    public class HashTest
    {
        [TestMethod]
        public void Hash_SHAH256_HashesAreEqual()
        {
            var valueToHash = "abc123";

            var hash1 = Hash.SHA256(valueToHash);
            var hash2 = Hash.SHA256(valueToHash);

            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void Hash_SHAH256_HashesAreNotEqual()
        {
            var valueToHash1 = "abc123";
            var valueToHash2 = "Abc123";

            var hash1 = Hash.SHA256(valueToHash1);
            var hash2 = Hash.SHA256(valueToHash2);

            Assert.AreNotEqual(hash1, hash2);
        }
    }
}