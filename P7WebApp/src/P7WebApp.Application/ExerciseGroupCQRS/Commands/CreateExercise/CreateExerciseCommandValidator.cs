using FluentValidation;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise
{
    public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(cec => cec.ExerciseGroupId)
                .NotNull().WithMessage("Exercise group id must be not be null.")
                .GreaterThan(0).WithMessage("Exercise group id cannot.");
            RuleFor(cec => cec.IsVisible)
                .NotNull().WithMessage("Must be specified if the exercise is visible.");
            RuleFor(cec => cec.Title)
                .NotNull().WithMessage("The title cannot be null.")
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(200).WithMessage("Title cannot be greater than 200 characters.");
            RuleFor(cec => cec.ExerciseNumber)
                .NotNull().WithMessage("Exercise number cannot be null.")
                .GreaterThan(0).WithMessage("It must be specified what exercise number the exercise has.");
        }
    }
}