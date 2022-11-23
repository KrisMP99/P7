using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.CreateExerciseGroup
{
    public class CreateExerciseGroupCommandValidation : AbstractValidator<CreateExerciseGroupCommand>
    {
        public CreateExerciseGroupCommandValidation()
        {
            RuleFor(ceg => ceg.CourseId)
                .NotEmpty().WithMessage("The exercisegroup must belong to a course (missing course id)");

            RuleFor(ceg => ceg.Title)
                .NotEmpty().WithMessage("Title is required");
        }
    }
}
