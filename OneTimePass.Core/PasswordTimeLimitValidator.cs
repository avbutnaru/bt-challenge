using OneTimePass.Core.Interfaces;
using System;

namespace OneTimePass.Core
{
    public class PasswordTimeLimitValidator : IPasswordValidator
    {
        private ICurrentTimeProvider _currentTimeProvider;
        private ISettingsProvider _settingsProvider;

        public PasswordTimeLimitValidator(ICurrentTimeProvider currentTimeProvider, ISettingsProvider settingsProvider)
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

            var passwordDuration = _settingsProvider.PasswordDurationInSeconds;
            var currentTime = _currentTimeProvider.Get();
            var createdAt = storedPassword.CreatedAt;
            if (createdAt.AddSeconds(passwordDuration) < currentTime)
            {
                return false;
            }

            return true;
        }
    }
}
