
using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.CreateCourse
{
    public class CreateCourseCommandValidation : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidation()
        {
            RuleFor(ccc => ccc.Title)
                .NotEmpty().WithMessage("Title is required");
        }
    }
}
