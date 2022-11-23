using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise
{
    public class CreateExerciseCommandValidation : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidation()
        {
            RuleFor(cec => cec.ExerciseGroupId)
                .NotEmpty().WithMessage("Could not find the exercisegroup id");

            RuleFor(cec => cec.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");


        }
    }
}
