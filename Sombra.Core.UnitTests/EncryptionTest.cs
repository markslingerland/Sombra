using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sombra.Core.UnitTests
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        public void Encryption_ValidatePassword_Returns_True()
        {
            var password = "abc123";
            var passwordToVerify = "abc123";
            var encryptedPassword = Encryption.CreateHash(password);

            var passwordIsCorrect = Encryption.ValidatePassword(passwordToVerify, encryptedPassword);
            
            Assert.IsTrue(passwordIsCorrect);
        }

        [TestMethod]
        public void Encryption_ValidatePassword_Returns_False()
        {
            var password = "Abc123";
            var passwordToVerify = "abc123";
            var encryptedPassword = Encryption.CreateHash(password);

            var passwordIsCorrect = Encryption.ValidatePassword(passwordToVerify, encryptedPassword);

            Assert.IsFalse(passwordIsCorrect);
        }

        [TestMethod]
        public void Encryption_CreateHash_ReturnsUnique()
        {
            var password = "abc123";

            var encryptedPassword1 = Encryption.CreateHash(password);
            var encryptedPassword2 = Encryption.CreateHash(password);

            Assert.AreNotEqual(encryptedPassword1, encryptedPassword2);
        }
    }
}