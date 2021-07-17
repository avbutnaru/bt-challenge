using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OneTimePass.Core;
using OneTimePass.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace OneTimePass.Service.Tests
{
    [TestClass]
    public class ValidatePasswordTaskTests
    {
        private string _password;
        private StoredPassword _passwordDetails;
        private DateTime _currentTime;
        private Mock<IPasswordStore> _passwordStore;
        private Mock<IMarkPasswordUsed> _markPassUsed;
        private Mock<IPasswordValidator> _validator1;
        private Mock<IPasswordValidator> _validator2;
        private List<IPasswordValidator> _validators;
        private ValidatePasswordTask _validatePasswordTask;

        [TestInitialize]
        public void Setup()
        {
            _password = "dsgkjsdklgjds";
            _passwordDetails = new StoredPassword(_password, DateTime.Now);

            _passwordStore = new Mock<IPasswordStore>();
            _passwordStore.Setup(p => p.GetPasswordDetails(_password)).Returns(_passwordDetails);

            _markPassUsed = new Mock<IMarkPasswordUsed>();

            _validator1 = new Mock<IPasswordValidator>();
            _validator2 = new Mock<IPasswordValidator>();
            _validators = new List<IPasswordValidator> { _validator1.Object, _validator2.Object };

            _validatePasswordTask = new ValidatePasswordTask(_passwordStore.Object, _validators, _markPassUsed.Object);
        }

        [TestMethod]
        public void InvalidPassword()
        {
            _validator1.Setup(p => p.IsValid(_passwordDetails)).Returns(true);
            _validator2.Setup(p => p.IsValid(_passwordDetails)).Returns(false);

            var result = _validatePasswordTask.IsValid(_password);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ValidPassword()
        {
            _validator1.Setup(p => p.IsValid(_passwordDetails)).Returns(true);
            _validator2.Setup(p => p.IsValid(_passwordDetails)).Returns(true);

            var result = _validatePasswordTask.IsValid(_password);

            Assert.AreEqual(true, result);
            _markPassUsed.Verify(s => s.Mark(_passwordDetails), Times.Once());
        }
    }
}
