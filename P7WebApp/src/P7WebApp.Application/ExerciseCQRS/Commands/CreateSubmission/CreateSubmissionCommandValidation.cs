using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateSubmission
{
    public class CreateSubmissionCommandValidation : AbstractValidator<CreateSubmissionCommand>
    {
        public CreateSubmissionCommandValidation()
        {
            RuleFor(csc => csc.ExerciseId)
                .NotEmpty().WithMessage("Exercise id must be provided")
                .NotNull().WithMessage("Exercise id cannot be null")
                .GreaterThan(0).WithMessage("Exercise id cannot be negative");
            
            RuleFor(csc => csc.UserId)
                .NotEmpty().WithMessage("User id must be provided")
                .NotNull().WithMessage("User id cannot be null")
                .GreaterThan(0).WithMessage("User id cannot be negative");

            RuleFor(csc => csc.Title)
                .NotEmpty().WithMessage("Title is required")
                .NotNull().WithMessage("Title cannot be null")
                .MaximumLength(200).WithMessage("Title cannot be greater than 200 characters");

        }
    }
}
