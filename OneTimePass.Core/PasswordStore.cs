using OneTimePass.Core.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace OneTimePass.Core
{

    public class PasswordStoreInMemory : IPasswordStore
    {
        private ICurrentTimeProvider _currentTimeProvider;
        private IDictionary<string, StoredPassword> _storedPasswords;

        public PasswordStoreInMemory(ICurrentTimeProvider currentTimeProvider)
        {
            _currentTimeProvider = currentTimeProvider;

            _storedPasswords = new Dictionary<string, StoredPassword>();
        }

        public StoredPassword GetPasswordDetails(string pass)
        {
            if (!_storedPasswords.ContainsKey(pass))
            {
                return null;
            }

            return _storedPasswords[pass];
        }

        public void StorePassword(string pass)
        {
            _storedPasswords.Add(pass, new StoredPassword(pass, _currentTimeProvider.Get()));
        }

        public void StorePassword(StoredPassword pass)
        {
            _storedPasswords[pass.Password] = pass;
        }
    }
}
