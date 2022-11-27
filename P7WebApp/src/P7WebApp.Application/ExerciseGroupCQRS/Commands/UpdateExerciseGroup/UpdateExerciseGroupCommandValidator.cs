using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.UpdateExercise
{
    public class UpdateExerciseGroupCommandValidator : AbstractValidator<UpdateExerciseGroupCommand>
    {
        public UpdateExerciseGroupCommandValidator()
        {
            RuleFor(ueg => ueg.Id)
                .NotEmpty().WithMessage("Exercise group id must be provided.")
                .NotNull().WithMessage("Exercise group id cannot be null.")
                .GreaterThan(0).WithMessage("Exercise group id cannot be negative.");

            RuleFor(ueg => ueg.Title)
                .NotEmpty().WithMessage("Title is required.")
                .NotNull().WithMessage("Title cannot be null.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(ueg => ueg.Description)
                .NotEmpty().WithMessage("Description is required.")
                .NotNull().WithMessage("Description cannot be null.");

            RuleFor(ueg => ueg.ExerciseGroupNumber)
                .GreaterThan(0).WithMessage("Exercise group number cannot be negative.");
        }
    }
}
