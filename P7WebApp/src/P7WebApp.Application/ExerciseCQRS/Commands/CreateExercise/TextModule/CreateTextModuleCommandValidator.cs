using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateExercise.TextModule
{
    public class CreateTextModuleCommandValidator : AbstractValidator<CreateTextModuleCommand>
    {
        public CreateTextModuleCommandValidator()
        {
            RuleFor(ctm => ctm.Text).NotNull().WithMessage("Not a valid text");
        }
    }
}
