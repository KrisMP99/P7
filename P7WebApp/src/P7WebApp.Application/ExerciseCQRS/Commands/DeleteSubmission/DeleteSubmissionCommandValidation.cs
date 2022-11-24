using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.Commands.DeleteSubmission
{
    public class DeleteSubmissionCommandValidation : AbstractValidator<DeleteSubmissionCommand>
    {
        public DeleteSubmissionCommandValidation()
        {
            RuleFor(dsc => dsc.ExerciseId)
                .NotEmpty().WithMessage("Exercise id must be provided")
                .NotNull().WithMessage("Exercise id cannot be null")
                .GreaterThan(0).WithMessage("Exericse id cannot be negative");

            RuleFor(dsc => dsc.SubmissionId)
                .NotEmpty().WithMessage("Submission id must be provided")
                .NotNull().WithMessage("Submission id cannot be null")
                .GreaterThan(0).WithMessage("Submission id cannot be negative");
        }
    }
}
