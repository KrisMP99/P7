using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.CodeModule
{
    public class UpdateCodeEditorModuleCommandValidator : AbstractValidator<UpdateCodeEditorModuleCommand>
    {
        public CreateCodeEditorModuleCommandValidator()
        {
            RuleFor(cmc => cmc.Height)
                .NotNull().WithMessage("Must be a valid height.");

            RuleFor(cmc => cmc.Width)
                .NotNull().WithMessage("Must be a valid weidth.");

            RuleFor(cmc => cmc.Position)
                .NotNull().WithMessage("Must be a valid position.");

            RuleFor(cmc => cmc.Description)
                .NotNull().WithMessage("Must be a valid description.");

            RuleFor(ccm => ccm.Code)
                .NotNull().WithMessage("Code cannot be null.");
        }
    }
}