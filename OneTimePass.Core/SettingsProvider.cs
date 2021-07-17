using OneTimePass.Core.Interfaces;
using System;

namespace OneTimePass.Core
{

    public class SettingsProvider : ISettingsProvider
    {
        public int PasswordDurationInSeconds => 30;
    }
}
