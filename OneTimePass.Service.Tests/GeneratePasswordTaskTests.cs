using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OneTimePass.Core;
using OneTimePass.Core.Interfaces;
using System;

namespace OneTimePass.Service.Tests
{
    [TestClass]
    public class GeneratePasswordTaskTests
    {
        private Mock<IUserProvider> _userProvider;
        private Mock<ICurrentTimeProvider> _timeProvider;
        private Mock<IPasswordGenerator> _passwordGenerator;
        private string _password;
        private Guid _userId;
        private DateTime _currentTime;

        [TestInitialize]
        public void Setup()
        {
            _password = "test";
            _userId = Guid.NewGuid();
            _currentTime = DateTime.Now;

            _userProvider = new Mock<IUserProvider>();
            _userProvider.Setup(p => p.GetUserId()).Returns(_userId);

            _timeProvider = new Mock<ICurrentTimeProvider>();
            _timeProvider.Setup(p => p.Get()).Returns(_currentTime);

            _passwordGenerator = new Mock<IPasswordGenerator>();
            _passwordGenerator.Setup(p => p.GeneratePass(_userId, _currentTime)).Returns(_password);
        }

        [TestMethod]
        public void GeneratePassword()
        {
            IPasswordStore passStore = new PasswordStoreInMemory(_timeProvider.Object);

            var task = new GeneratePasswordTask(_passwordGenerator.Object, passStore, _userProvider.Object, _timeProvider.Object);
            var generatedPass = task.Generate();

            _passwordGenerator.Verify(s => s.GeneratePass(_userId, _currentTime), Times.Once());
            Assert.AreEqual(_password, generatedPass);
        }

        [TestMethod]
        public void StorePassword()
        {
            var passStore = new Mock<IPasswordStore>();

            var task = new GeneratePasswordTask(_passwordGenerator.Object, passStore.Object, _userProvider.Object, _timeProvider.Object);
            var generatedPass = task.Generate();

            passStore.Verify(s => s.StorePassword(_password), Times.Once());
        }
    }
}
