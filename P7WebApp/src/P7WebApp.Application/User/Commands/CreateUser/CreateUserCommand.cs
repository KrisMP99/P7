﻿using MediatR;
using P7WebApp.Application.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace P7WebApp.Application.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result>
    {
        public CreateUserCommand(string username, string password, string email, string firstName, string lastName)
        {
            Username = username;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Username { get; }

        [DataType(DataType.Password)]
        public string Password { get;}

        [DataType(DataType.EmailAddress)]
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}