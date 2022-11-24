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
                .NotEmpty().WithMessage("Exercisegroup id must be provided")
                .NotNull().WithMessage("Exercisegroup id cannot be null")
                .GreaterThan(0).WithMessage("Exercisegroup id cannot be negative");

            RuleFor(uec => uec.Id)
                .NotEmpty().WithMessage("Exercise id must be provided")
                .NotNull().WithMessage("Exercise id cannot be null")
                .GreaterThan(0).WithMessage("Exercise id cannot be negative");

            RuleFor(uec => uec.Title)
                .NotEmpty().WithMessage("Title is required")
                .NotNull().WithMessage("Title cannot be null")
                .MaximumLength(200).WithMessage("Title cannot be greater than 200 characters");

            RuleFor(uec => uec.ExerciseNumber)
                .NotNull().WithMessage("Exercisenumber cannot be null")
                .GreaterThan(0).WithMessage("Exercisenumber cannot be negative");
        }
    }
}
