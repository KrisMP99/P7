using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.DeleteExerciseGroup
{
    public class DeleteExerciseGroupCommandValidation : AbstractValidator<DeleteExerciseGroupCommand>
    {
        public DeleteExerciseGroupCommandValidation()
        {
            RuleFor(deg => deg.ExerciseGroupId)
                .NotEmpty().WithMessage("Exercisegroup id must be provided")
                .NotNull().WithMessage("Exercisegroup id cannot be null")
                .GreaterThan(0).WithMessage("Exercisegroup id cannot be negative");

            RuleFor(deg => deg.CourseId)
                .NotEmpty().WithMessage("Course id must be provided")
                .NotNull().WithMessage("Course id cannot be null")
                .GreaterThan(0).WithMessage("Course id cannot be negative");
        }
    }
}
