using MediatR;

namespace P7WebApp.Application.ProfileCQRS.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<int>
    {
        public UpdateProfileCommand(string email, string firstName, string lastName, string password)
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
