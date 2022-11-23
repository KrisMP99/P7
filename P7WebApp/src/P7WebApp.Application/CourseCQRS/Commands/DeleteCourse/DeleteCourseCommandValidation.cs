using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.DeleteCourse
{
    public class DeleteCourseCommandValidation : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidation()
        {
            RuleFor(dcc => dcc.Id)
                .NotEmpty().WithMessage("The course to be deleted must have an id (missing course id)");
        }
    }
}
