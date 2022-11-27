﻿using FluentValidation;

namespace P7WebApp.Application.ProfileCQRS.Commands.SignIn
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(u => u.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}