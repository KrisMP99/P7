using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateExercise.CodeModule
{
    public class CreateCodeModuleCommandValidator : AbstractValidator<CreateCodeModuleCommand>
    {
        public CreateCodeModuleCommandValidator()
        {
            RuleFor(ccm => ccm.Code).NotNull().WithMessage("Code cannot be null");
        }
    }
}