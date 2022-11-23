using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateSolution
{
    public class CreateSolutionCommandValidation : AbstractValidator<CreateSolutionCommand>
    {
        public CreateSolutionCommandValidation()
        {
            RuleFor(csc => csc.ExerciseId)
                .NotEmpty().WithMessage("Exercise id is empty");

            RuleFor(csc => csc.VisibleFromDate)
                .NotEmpty().WithMessage("Visible from date cannot be empty")
                .GreaterThanOrEqualTo(csc => DateTime.Now).WithMessage("Visible from date cannot be less than current time");
        }
    }
}
