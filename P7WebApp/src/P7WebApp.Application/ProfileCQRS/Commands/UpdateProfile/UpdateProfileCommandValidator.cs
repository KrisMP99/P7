using FluentValidation;

namespace P7WebApp.Application.ProfileCQRS.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(uap => uap.Email).NotEmpty().EmailAddress().WithMessage("Must be an email");
            RuleFor(uap => uap.FirstName).NotEmpty().WithMessage("Firstname must be set");
            RuleFor(uap => uap.LastName).NotEmpty().WithMessage("LastName must be set");
            RuleFor(uap => uap.Email).NotEmpty().WithMessage("Password must be set");
        }
    }
}