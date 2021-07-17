using System;

namespace OneTimePass.Core
{
    public class StoredPassword
    {
        public StoredPassword(string pass, DateTime createdAt)
        {
            Password = pass;
            CreatedAt = createdAt;
            HasBeenUsed = false;
        }

        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool HasBeenUsed { get; set; }
    }
}
