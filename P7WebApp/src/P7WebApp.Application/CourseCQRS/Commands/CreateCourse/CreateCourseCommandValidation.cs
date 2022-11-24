
using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.CreateCourse
{
    public class CreateCourseCommandValidation : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidation()
        {
            RuleFor(ccc => ccc.Title)
                .NotEmpty().WithMessage("Title is required")
                .NotNull().WithMessage("Title cannot be null")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(ccc => ccc.Description)
                .NotNull().WithMessage("Description cannot be null");
        }
    }
}
