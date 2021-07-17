using OneTimePass.Core.Interfaces;
using System;

namespace OneTimePass.Service
{
    public class GeneratePasswordTask
    {
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IPasswordStore _passwordStore;
        private readonly IUserProvider _userProvider;
        private readonly ICurrentTimeProvider _currentTimeProvider;

        public GeneratePasswordTask(IPasswordGenerator passwordGenerator, IPasswordStore passwordStore, IUserProvider userProvider, ICurrentTimeProvider currentTimeProvider)
        {
            this._passwordGenerator = passwordGenerator;
            this._passwordStore = passwordStore;
            this._userProvider = userProvider;
            this._currentTimeProvider = currentTimeProvider;
        }

        public string Generate()
        {
            var userId = _userProvider.GetUserId();
            var currentTime = _currentTimeProvider.Get();
            var newPassword = _passwordGenerator.GeneratePass(userId, currentTime);
            
            _passwordStore.StorePassword(newPassword);

            return newPassword;
        }
    }
}
