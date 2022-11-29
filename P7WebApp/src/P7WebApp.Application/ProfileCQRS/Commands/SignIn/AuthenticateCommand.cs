using MediatR;
using P7WebApp.Application.Responses.Profile;

namespace P7WebApp.Application.ProfileCQRS.Commands.SignIn
{
    public class AuthenticateCommand : IRequest<TokenResponse>
    {
        public AuthenticateCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}