using MediatR;

namespace P7WebApp.Application.User.Commands
{
    public class SignInCommand : IRequest<int>
    {
        public string Username { get; }
        public string Password { get; }
    }
}
