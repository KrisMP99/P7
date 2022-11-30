using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.TextModule
{
    public class UpdateTextModuleCommandValidator : AbstractValidator<UpdateTextModuleCommand>
    {
        public UpdateTextModuleCommandValidator()
        {
            RuleFor(ctm => ctm.Title)
                .NotNull().WithMessage("Not a valid text.");

            RuleFor(ctm => ctm.Content)
                .NotNull().WithMessage("Not valid content.");

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
