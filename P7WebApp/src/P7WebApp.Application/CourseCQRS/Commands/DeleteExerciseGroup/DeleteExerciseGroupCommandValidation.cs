using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.DeleteExerciseGroup
{
    public class DeleteExerciseGroupCommandValidation : AbstractValidator<DeleteExerciseGroupCommand>
    {
        public DeleteExerciseGroupCommandValidation()
        {
            RuleFor(deg => deg.ExerciseGroupId)
                .NotEmpty().WithMessage("The exercisegroup to be deleted must have an id (missing exercisegroup id)"); 
        }
    }
}
