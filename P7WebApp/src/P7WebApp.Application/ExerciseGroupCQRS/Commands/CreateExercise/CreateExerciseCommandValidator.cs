using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise
{
    public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(cec => cec.ExerciseGroupId)
                .NotEmpty().WithMessage("Exercise group id must be provided.")
                .NotNull().WithMessage("Exercise group id cannot be null.")
                .GreaterThan(0).WithMessage("Exercise group id cannot be negative.");

            RuleFor(cec => cec.Title)
                .NotEmpty().WithMessage("Title is required.")
                .NotNull().WithMessage("Title cannot be null.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(cec => cec.ExerciseNumber)
                .GreaterThan(0).WithMessage("Exercise number cannot be negative.");
        }
    }
}
