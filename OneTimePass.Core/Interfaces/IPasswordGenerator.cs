using System;

namespace OneTimePass.Core.Interfaces
{
    public interface IPasswordGenerator
    {
        string GeneratePass(Guid userId, DateTime date);
    }
}