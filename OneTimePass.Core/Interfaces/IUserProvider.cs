using System;

namespace OneTimePass.Core.Interfaces
{
    public interface IUserProvider
    {
        Guid GetUserId();
    }
}
