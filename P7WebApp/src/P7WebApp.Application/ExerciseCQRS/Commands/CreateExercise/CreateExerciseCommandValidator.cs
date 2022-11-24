using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateExercise
{
    public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(cec => cec.ExerciseGroupId).NotNull().GreaterThan(0).WithMessage("Must be a valid exercise group number.");
            RuleFor(cec => cec.IsVisible).NotNull().WithMessage("Must be specified if the exercise is visible");
            RuleFor(cec => cec.Title).NotNull().NotEmpty().WithMessage("Title must be set.");
            RuleFor(cec => cec.ExerciseNumber).NotNull().GreaterThan(0).WithMessage("It must be specified what exercise number the exercise has.");
        }
    }
}