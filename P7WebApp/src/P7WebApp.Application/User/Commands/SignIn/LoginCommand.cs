using MediatR;
using P7WebApp.Application.Common.Models;

namespace P7WebApp.Application.User.Commands.SignIn
{
    public class LoginCommand : IRequest<Result>
    {
        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}