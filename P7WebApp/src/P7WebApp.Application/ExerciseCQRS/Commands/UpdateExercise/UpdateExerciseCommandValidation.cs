using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise
{
    public class UpdateExerciseCommandValidation : AbstractValidator<UpdateExerciseCommand>
    {
        public UpdateExerciseCommandValidation()
        {
            RuleFor(uec => uec.ExerciseGroupId)
                .NotEmpty().WithMessage("Exercisegroup id must be provided");
        }
    }
}
