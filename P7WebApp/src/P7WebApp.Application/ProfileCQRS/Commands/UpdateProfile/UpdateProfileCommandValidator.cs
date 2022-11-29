using FluentValidation;

namespace P7WebApp.Application.ProfileCQRS.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(uap => uap.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .NotNull().WithMessage("Email must be defined.")
                .EmailAddress().WithMessage("Must be an email.");

            RuleFor(uap => uap.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty.")
                .NotNull().WithMessage("First name must be valid.");

            RuleFor(uap => uap.LastName)
                .NotEmpty().WithMessage("Last name cannot be empty.")
                .NotNull().WithMessage("Last name must be valid.");
        }
    }
}