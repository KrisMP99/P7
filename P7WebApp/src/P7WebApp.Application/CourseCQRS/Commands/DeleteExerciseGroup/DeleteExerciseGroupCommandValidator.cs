using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.DeleteExerciseGroup
{
    public class DeleteExerciseGroupCommandValidator : AbstractValidator<DeleteExerciseGroupCommand>
    {
        public DeleteExerciseGroupCommandValidator()
        {
            RuleFor(deg => deg.ExerciseGroupId)
                .NotEmpty().WithMessage("Exercise group id must be provided.")
                .NotNull().WithMessage("Exercise group id cannot be null.")
                .GreaterThan(0).WithMessage("Exercise group id cannot be negative.");

            RuleFor(deg => deg.CourseId)
                .NotEmpty().WithMessage("Course id must be provided.")
                .NotNull().WithMessage("Course id cannot be null.")
                .GreaterThan(0).WithMessage("Course id cannot be negative.");
        }
    }
}
