using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Queries.Get
{
    public class GetExerciseQueryValidator : AbstractValidator<GetExerciseQuery>
    {
        public GetExerciseQueryValidator()
        {
            RuleFor(geq => geq.ExerciseGroupId)
                .NotEmpty().WithMessage("Exercise group id cannot be 0")
                .NotNull().WithMessage("Must be a valid exercise group id");

            RuleFor(geq => geq.ExerciseId)
                .NotEmpty().WithMessage("Exercise id cannot be 0")
                .NotNull().WithMessage("Must be a valid exercise id");
        }
    }
}