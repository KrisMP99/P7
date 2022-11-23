namespace P7WebApp.Domain.Exceptions
{
    public class AccountException : Exception
    {
        public AccountException(string? message) : base(message)
        {
        }
    }
}