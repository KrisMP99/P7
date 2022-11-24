using MediatR;

namespace P7WebApp.Application.AccountCQRS.Commands.UpdateAccountProfile
{
    public class UpdateAccountProfileCommand : IRequest<int>
    {
        public UpdateAccountProfileCommand(string email, string firstName, string lastName, string password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Password { get; }
    }
}
