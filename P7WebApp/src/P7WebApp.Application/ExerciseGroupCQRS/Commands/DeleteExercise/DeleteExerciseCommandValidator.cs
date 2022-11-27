using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.DeleteExercise
{
    public class DeleteExerciseCommandValidator : AbstractValidator<DeleteExerciseCommand>
    {
        public DeleteExerciseCommandValidator()
        {
            RuleFor(dec => dec.Id)
                .NotEmpty().WithMessage("Exercise id must be provided.")
                .NotNull().WithMessage("Exercise id cannot be null.")
                .GreaterThan(0).WithMessage("Exercise id cannot be negative.");

            RuleFor(dec => dec.ExerciseGroupId)
                .NotEmpty().WithMessage("Exercise group id must be provided.")
                .NotNull().WithMessage("Exercise group id cannot be null.")
                .GreaterThan(0).WithMessage("Exercise group id cannot be negative.");
        }
    }
}
