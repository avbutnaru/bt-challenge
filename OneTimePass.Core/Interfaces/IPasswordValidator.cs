using System;

namespace OneTimePass.Core.Interfaces
{
    public interface IPasswordValidator
    {
        bool IsValid(StoredPassword password);
    }
}
