using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateSolution
{
    public class CreateSolutionCommandValidator : AbstractValidator<CreateSolutionCommand>
    {
        public CreateSolutionCommandValidator()
        {
            RuleFor(csc => csc.ExerciseId)
                .NotEmpty().WithMessage("Exercise id must be provided.")
                .NotNull().WithMessage("Exercise id cannot be null.")
                .GreaterThan(0).WithMessage("Exercise id cannot be negative.");

            RuleFor(csc => csc.VisibleFromDate)
                .NotEmpty().WithMessage("Visible from date cannot be empty.")
                .GreaterThanOrEqualTo(csc => DateTime.Now).WithMessage("Visible from date cannot be less than current time.");
        }
    }
}
