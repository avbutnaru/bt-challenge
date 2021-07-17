using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OneTimePass;
using OneTimePass.Core.Interfaces;
using System;

namespace OneTimePass.Core.Tests
{
    [TestClass]
    public class PasswordTimeLimitValidatorTests
    {
        private DateTime _currentTime;
        private Mock<ICurrentTimeProvider> _currentTimeProvider;
        private ISettingsProvider _settingsProvider;
        private PasswordTimeLimitValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _currentTime = DateTime.Now;
            _currentTimeProvider = new Mock<ICurrentTimeProvider>();
            _currentTimeProvider.Setup(p => p.Get()).Returns(_currentTime);

            _settingsProvider = new SettingsProvider();

            _validator = new PasswordTimeLimitValidator(_currentTimeProvider.Object, _settingsProvider);
        }

        [TestMethod]
        public void NullPasswordWithTimeLimitIsInvalid()
        {
            var result = _validator.IsValid(null);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void PasswordTimeLimitIsInvalid()
        {
            var passwordDetails = new StoredPassword("test", _currentTime.AddSeconds(-_settingsProvider.PasswordDurationInSeconds - 1));

            var result = _validator.IsValid(passwordDetails);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void PasswordTimeLimitIsValidForMaximumDuration()
        {
            var passwordDetails = new StoredPassword("test", _currentTime.AddSeconds(-_settingsProvider.PasswordDurationInSeconds));

            var result = _validator.IsValid(passwordDetails);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void PasswordTimeLimitIsValid()
        {
            var passwordDetails = new StoredPassword("test", _currentTime.AddSeconds(-_settingsProvider.PasswordDurationInSeconds + 3));

            var result = _validator.IsValid(passwordDetails);

            Assert.AreEqual(true, result);
        }
    }
}
