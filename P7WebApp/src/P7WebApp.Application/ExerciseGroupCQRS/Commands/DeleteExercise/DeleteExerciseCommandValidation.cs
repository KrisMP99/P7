using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.DeleteExercise
{
    public class DeleteExerciseCommandValidation : AbstractValidator<DeleteExerciseCommand>
    {
        public DeleteExerciseCommandValidation()
        {
            RuleFor(dec => dec.Id)
                .NotEmpty().WithMessage("Could not find the exercise id");

            RuleFor(dec => dec.ExerciseGroupId)
                .NotEmpty().WithMessage("Could not find the exercisegroup id");
        }
    }
}
