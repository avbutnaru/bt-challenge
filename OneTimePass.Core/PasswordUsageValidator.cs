using OneTimePass.Core.Interfaces;
using System;

namespace OneTimePass.Core
{
    public class PasswordUsageValidator : IPasswordValidator
    {
        private ICurrentTimeProvider _currentTimeProvider;
        private ISettingsProvider _settingsProvider;

        public PasswordUsageValidator(ICurrentTimeProvider currentTimeProvider, ISettingsProvider settingsProvider)
        {
            _currentTimeProvider = currentTimeProvider;
            _settingsProvider = settingsProvider;
        }

        public bool IsValid(StoredPassword storedPassword)
        {
            if (storedPassword == null)
            {
                return false;
            }

            return !storedPassword.HasBeenUsed;
        }
    }
}
