using FluentValidation;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(cmc => cmc.Height)
                .NotNull().WithMessage("Must be a valid height.");

            RuleFor(cmc => cmc.Width)
                .NotNull().WithMessage("Must be a valid weidth.");

            RuleFor(cmc => cmc.Position)
                .NotNull().WithMessage("Must be a valid position.");

            RuleFor(cmc => cmc.Description)
                .NotNull().WithMessage("Must be a valid description.");

            RuleFor(cmc => cmc.Type)
                .NotEmpty().WithMessage("There must be assigned which type the module has.")
                .NotNull().WithMessage("Type cannot be null");
        }
    }
}