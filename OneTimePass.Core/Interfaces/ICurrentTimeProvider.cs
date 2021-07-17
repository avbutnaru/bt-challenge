using System;

namespace OneTimePass.Core.Interfaces
{
    public interface ICurrentTimeProvider
    {
        DateTime Get();
    }
}
