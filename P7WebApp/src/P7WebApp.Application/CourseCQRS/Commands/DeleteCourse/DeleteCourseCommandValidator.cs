using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.DeleteCourse
{
    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidator()
        {
            RuleFor(dcc => dcc.Id)
                .NotEmpty().WithMessage("Course id must be provided.")
                .NotNull().WithMessage("Course id cannot be null.")
                .GreaterThan(0).WithMessage("Course id cannot be negative.");
        }
    }
}
