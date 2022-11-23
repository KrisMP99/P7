using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.Commands.DeleteSolution
{
    public class DeleteSolutionCommandValidation : AbstractValidator<DeleteSolutionCommand>
    {
        public DeleteSolutionCommandValidation()
        {
            RuleFor(dsc => dsc.ExerciseId)
                .NotEmpty().WithMessage("Exercise id cannot be empty");

            RuleFor(dsc => dsc.SolutionId)
                .NotEmpty().WithMessage("Solution id cannot be empty");
        }
    }
}
