using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(ucc => ucc.Id)
                .NotEmpty().WithMessage("Course id must be provided.")
                .NotNull().WithMessage("Course id cannot be null.")
                .GreaterThan(0).WithMessage("Course id cannot be negative.");

            RuleFor(ucc => ucc.Title)
                .NotEmpty().WithMessage("Title is required.")
                .NotNull().WithMessage("Title cannot be null.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(ucc => ucc.Description)
                .NotNull().WithMessage("Description cannot be null.");
        }
    }
}
