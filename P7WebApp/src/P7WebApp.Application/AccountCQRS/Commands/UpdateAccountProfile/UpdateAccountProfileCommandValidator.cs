using FluentValidation;

namespace P7WebApp.Application.AccountCQRS.Commands.UpdateAccountProfile
{
    public class UpdateAccountProfileCommandValidator : AbstractValidator<UpdateAccountProfileCommand>
    {
        public UpdateAccountProfileCommandValidator()
        {
            RuleFor(uap => uap.Email).NotEmpty().EmailAddress().WithMessage("Must be an email");
            RuleFor(uap => uap.FirstName).NotEmpty().WithMessage("Firstname must be set");
            RuleFor(uap => uap.LastName).NotEmpty().WithMessage("LastName must be set");
            RuleFor(uap => uap.Email).NotEmpty().WithMessage("Password must be set");
        }
    }
}