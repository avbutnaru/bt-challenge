namespace OneTimePass.Core.Interfaces
{
    public interface ISettingsProvider
    {
        int PasswordDurationInSeconds { get; }
    }
}
