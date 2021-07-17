using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OneTimePass.Core.Interfaces;
using System;

namespace OneTimePass.Core.Tests
{
    [TestClass]
    public class PasswordStoreTests
    {
        IPasswordStore _passwordStore;
        private string _password;
        private DateTime _passwordSetTime;
        private Mock<ICurrentTimeProvider> _currentTimeProvider;

        [TestInitialize]
        public void Setup()
        {
            _password = "jkfhsdkjfsdhf";
            _passwordSetTime = DateTime.Now;
            _currentTimeProvider = new Mock<ICurrentTimeProvider>();
            _currentTimeProvider.Setup(p => p.Get()).Returns(_passwordSetTime);

            _passwordStore = new PasswordStoreInMemory(_currentTimeProvider.Object);
        }

        [TestMethod]
        public void StorePassword()
        {
            _passwordStore.StorePassword(_password);
            var result = _passwordStore.GetPasswordDetails(_password);

            _currentTimeProvider.Verify(s => s.Get(), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Password, _password);
            Assert.AreEqual(result.CreatedAt, _passwordSetTime);
        }

        [TestMethod]
        public void GetPasswordDetailsWhenNotExists()
        {
            var result = _passwordStore.GetPasswordDetails(_password);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void StoreUsedPassword()
        {
            _passwordStore.StorePassword(_password);
            
            var existingPass = _passwordStore.GetPasswordDetails(_password);
            existingPass.HasBeenUsed = true;

            _passwordStore.StorePassword(existingPass);
            var existingPassUpdated = _passwordStore.GetPasswordDetails(_password);

            Assert.IsNotNull(existingPassUpdated);
            Assert.AreEqual(existingPassUpdated.Password, _password);
            Assert.AreEqual(existingPassUpdated.CreatedAt, _passwordSetTime);
            Assert.AreEqual(existingPassUpdated.HasBeenUsed, true);
        }
    }
}
