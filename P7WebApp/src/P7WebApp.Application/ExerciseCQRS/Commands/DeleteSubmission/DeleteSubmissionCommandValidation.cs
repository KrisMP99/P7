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
                .NotEmpty().WithMessage("Exercise id must be provided");

            RuleFor(dsc => dsc.SubmissionId)
                .NotEmpty().WithMessage("Exercise id must be provided");
        }
    }
}
