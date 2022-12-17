using FluentValidation;

namespace P7WebApp.Application.ProfileCQRS.Commands.CreateProfile
{
    public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileCommandValidator()
        {
            RuleFor(crm => crm.Username)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .NotNull().WithMessage("Username must be valid.");

            RuleFor(crm => crm.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .NotNull().WithMessage("Password must be valid.");

            RuleFor(crm => crm.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .NotNull().WithMessage("Email must be defined.")
                .EmailAddress().WithMessage("Email must be a valid email.");

            RuleFor(crm => crm.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty.")
                .NotNull().WithMessage("First name must be valid.");

            RuleFor(crm => crm.LastName)
                .NotEmpty().WithMessage("Last name cannot be empty.")
                .NotNull().WithMessage("Last name must be valid.");
        }
    }
}