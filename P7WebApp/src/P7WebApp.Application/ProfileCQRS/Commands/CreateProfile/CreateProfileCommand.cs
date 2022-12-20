using MediatR;
using P7WebApp.Application.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace P7WebApp.Application.ProfileCQRS.Commands.CreateProfile;

public class CreateProfileCommand : IRequest<int>
{
    public CreateProfileCommand(string username, string password, string email, string firstName, string lastName)
    {
        Username = username;
        Password = password;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Username { get; }

    [DataType(DataType.Password)]
    public string Password { get; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
}