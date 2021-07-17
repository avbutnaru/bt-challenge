using OneTimePass.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneTimePass.Core
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public const string DateFormatForPass = "MMddyyHmmss";

        public string GeneratePass(Guid userId, DateTime date)
        {
            return userId + date.ToString(DateFormatForPass);
        }
    }
}
