using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.UpdateExercise
{
    public class UpdateExerciseGroupCommandValidation : AbstractValidator<UpdateExerciseGroupCommand>
    {
        public UpdateExerciseGroupCommandValidation()
        {
            RuleFor(ueg => ueg.Id)
                .NotEmpty().WithMessage("Could not find the exercisegroup id");
        }
    }
}
