namespace OneTimePass.Core.Interfaces
{
    public interface IPasswordStore
    {
        void StorePassword(string pass);
        
        void StorePassword(StoredPassword pass);

        StoredPassword GetPasswordDetails(string pass);
    }
}
