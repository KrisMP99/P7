using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.Aggregates.AccountAggregate
{
    public class AccountProfile : ValueObject
    {
        public AccountProfile(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
    }
}