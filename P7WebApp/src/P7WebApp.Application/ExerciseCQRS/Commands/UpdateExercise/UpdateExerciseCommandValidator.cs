using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise
{
    public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
    {
        public UpdateExerciseCommandValidator()
        {
            RuleFor(uec => uec.ExerciseGroupId)
                .NotEmpty().WithMessage("Exercise group id must be provided.")
                .NotNull().WithMessage("Exercise group id cannot be null.")
                .GreaterThan(0).WithMessage("Exercise group id cannot be negative.");

            RuleFor(uec => uec.Id)
                .NotEmpty().WithMessage("Exercise id must be provided.")
                .NotNull().WithMessage("Exercise id cannot be null.")
                .GreaterThan(0).WithMessage("Exercise id cannot be negative.");

            RuleFor(uec => uec.Title)
                .NotEmpty().WithMessage("Title is required.")
                .NotNull().WithMessage("Title cannot be null.")
                .MaximumLength(200).WithMessage("Title cannot be greater than 200 characters.");

            RuleFor(uec => uec.ExerciseNumber)
                .NotNull().WithMessage("Exercise number cannot be null.")
                .GreaterThan(0).WithMessage("Exercise number cannot be negative.");
        }
    }
}