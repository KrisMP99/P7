using FluentValidation;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.QuizModule
{
    public class CreateQuizModuleCommandValidator : AbstractValidator<CreateQuizModuleCommand>
    {
        public CreateQuizModuleCommandValidator()
        {
            RuleFor(cmc => cmc.Height)
                .NotNull().WithMessage("Must be a valid height.");

            RuleFor(cmc => cmc.Width)
                .NotNull().WithMessage("Must be a valid weidth.");

            RuleFor(cmc => cmc.Position)
                .NotNull().WithMessage("Must be a valid position.");

            RuleFor(cmc => cmc.Description)
                .NotNull().WithMessage("Must be a valid description.");
        }
    }
}