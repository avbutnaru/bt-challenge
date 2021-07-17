using OneTimePass.Core.Interfaces;
using System;

namespace OneTimePass.Core
{

    public class CurrentTimeProvider : ICurrentTimeProvider
    {
        public DateTime Get()
        {
            return DateTime.Now;
        }
    }
}
