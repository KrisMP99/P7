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
                .NotEmpty().WithMessage("Exercise id cannot be empty");
            
            RuleFor(csc => csc.UserId)
                .NotEmpty().WithMessage("User id cannot be empty");

        }
    }
}
