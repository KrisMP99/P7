using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.Aggregates.AccountAggregate
{
    public class AccountProfile : ValueObject
    {
        public AccountProfile(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Password { get; }
    }
}