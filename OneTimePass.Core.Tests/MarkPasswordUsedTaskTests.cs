using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OneTimePass;
using OneTimePass.Core;
using OneTimePass.Core.Interfaces;
using System;

namespace OneTimePass.Core.Tests
{
    [TestClass]
    public class MarkPasswordUsedTaskTests
    {
        private StoredPassword _password;
        private Mock<IPasswordStore> _passwordStore;
        private MarkPasswordUsedTask _task;

        [TestInitialize]
        public void Setup()
        {
            _password = new StoredPassword("test", DateTime.Now);
            _passwordStore = new Mock<IPasswordStore>();
            _task = new MarkPasswordUsedTask(_passwordStore.Object);
        }

        [TestMethod]
        public void MarkPassword()
        {
            _task.Mark(_password);

            Assert.AreEqual(true, _password.HasBeenUsed);
            _passwordStore.Verify(s => s.StorePassword(_password), Times.Once());
        }
    }
}
