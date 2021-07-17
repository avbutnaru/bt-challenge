using System;

namespace OneTimePass.Core.Interfaces
{
    public interface IMarkPasswordUsed
    {
        void Mark(StoredPassword password);
    }
}
