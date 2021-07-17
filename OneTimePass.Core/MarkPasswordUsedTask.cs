using OneTimePass.Core.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace OneTimePass.Core
{
    public class MarkPasswordUsedTask : IMarkPasswordUsed
    {
        private IPasswordStore _passwordStore;

        public MarkPasswordUsedTask(IPasswordStore passwordStore)
        {
            _passwordStore = passwordStore;
        }

        public void Mark(StoredPassword password)
        {
            password.HasBeenUsed = true;

            _passwordStore.StorePassword(password);
        }
    }
}
