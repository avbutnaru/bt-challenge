using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OneTimePass;
using OneTimePass.Core;
using System;

namespace OneTimePass.Core.Tests
{
    [TestClass]
    public class PasswordUsageValidatorTests
    {
        private PasswordUsageValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new PasswordUsageValidator(new CurrentTimeProvider(), new SettingsProvider());
        }

        [TestMethod]
        public void NullPasswordWithUsageLimitIsInvalid()
        {
            var result = _validator.IsValid(null);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UsedPasswordIsInvalid()
        {
            var passwordDetails = new StoredPassword("test", DateTime.Now);
            passwordDetails.HasBeenUsed = true;

            var result = _validator.IsValid(passwordDetails);


            Assert.AreEqual(false, result);
        }
    }
}
