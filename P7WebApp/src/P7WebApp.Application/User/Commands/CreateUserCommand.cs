using MediatR;
using System.ComponentModel.DataAnnotations;

namespace P7WebApp.Application.User.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
