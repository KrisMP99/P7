
using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.CreateCourse
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(ccc => ccc.Title)
                .NotEmpty().WithMessage("Title is required.")
                .NotNull().WithMessage("Title cannot be null.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(ccc => ccc.Description)
                .NotNull().WithMessage("Description cannot be null.");
        }
    }
}
