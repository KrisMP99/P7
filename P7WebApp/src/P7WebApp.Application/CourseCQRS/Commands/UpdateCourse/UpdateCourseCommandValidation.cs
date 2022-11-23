using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidation : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidation()
        {
            RuleFor(ucc => ucc.Id)
                .NotEmpty().WithMessage("Missing course id"); 
        }
    }
}
