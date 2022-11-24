using FluentValidation;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.TextModule
{
    public class CreateTextModuleCommandValidator : AbstractValidator<CreateTextModuleCommand>
    {
        public CreateTextModuleCommandValidator()
        {
            RuleFor(ctm => ctm.Text)
                .NotNull().WithMessage("Not a valid text.");
            RuleFor(cmc => cmc.Height)
                .NotNull().WithMessage("Must be a valid height.");
            RuleFor(cmc => cmc.Width)
                .NotNull().WithMessage("Must be a valid weidth.");
            RuleFor(cmc => cmc.Position)
                .NotNull().WithMessage("Must be a valid position.");
            RuleFor(cmc => cmc.Description)
                .NotNull().WithMessage("Must be a valid description.")
                .MaximumLength(500).WithMessage("Description cannot be grater than 500 characters.");
        }
    }
}
