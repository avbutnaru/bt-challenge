using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OneTimePass;
using OneTimePass.Core;
using System;

namespace OneTimePass.Core.Tests
{
    [TestClass]
    public class PasswordGeneratorTests
    {
        [TestMethod]
        public void GeneratePassword()
        {
            var userId = Guid.NewGuid();
            var date = DateTime.Now;

            var passGenerator = new PasswordGenerator();
            var password = passGenerator.GeneratePass(userId, date);

            Assert.AreEqual(userId + date.ToString(PasswordGenerator.DateFormatForPass), password);
        }
    }
}
