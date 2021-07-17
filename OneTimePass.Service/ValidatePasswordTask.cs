using OneTimePass.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace OneTimePass.Service
{
    public class ValidatePasswordTask
    {
        private readonly IPasswordStore _passwordStore;
        private IList<IPasswordValidator> _validators;
        private readonly IMarkPasswordUsed _markPasswordUsed;

        public ValidatePasswordTask(IPasswordStore passwordStore, IList<IPasswordValidator> validators, IMarkPasswordUsed markPasswordUsed)
        {
            this._passwordStore = passwordStore;
            _validators = validators;
            this._markPasswordUsed = markPasswordUsed;
        }

        public bool IsValid(string pass)
        {
            var passwordDetails = _passwordStore.GetPasswordDetails(pass);

            foreach(var validator in _validators)
            {
                if (!validator.IsValid(passwordDetails))
                {
                    return false;
                }
            }

            _markPasswordUsed.Mark(passwordDetails);
            return true;
        }
    }
}
