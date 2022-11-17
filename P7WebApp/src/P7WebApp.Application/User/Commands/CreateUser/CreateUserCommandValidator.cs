﻿using FluentValidation;

namespace P7WebApp.Application.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(crm => crm.Username).NotEmpty().WithMessage("Must be a valid username");
            RuleFor(crm => crm.Password).NotEmpty().WithMessage("Must be a valid password");
            RuleFor(crm => crm.Email).NotEmpty().EmailAddress().WithMessage("Must be a valid email");
            RuleFor(crm => crm.FirstName).NotEmpty();
            RuleFor(crm => crm.LastName).NotEmpty();
        }
    }
}