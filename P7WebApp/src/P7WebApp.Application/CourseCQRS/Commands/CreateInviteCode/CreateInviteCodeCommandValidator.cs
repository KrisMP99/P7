using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.CreateInviteCode
{
    public class CreateInviteCodeCommandValidator : AbstractValidator<CreateInviteCodeCommand>
    {
        public CreateInviteCodeCommandValidator()
        {
            RuleFor(cic => cic.CourseId)
                .NotEmpty().WithMessage("Course id must be provided.")
                .NotNull().WithMessage("Course id cannot be null.")
                .GreaterThan(0).WithMessage("Course id cannot be negative.");
        }
    }
}
