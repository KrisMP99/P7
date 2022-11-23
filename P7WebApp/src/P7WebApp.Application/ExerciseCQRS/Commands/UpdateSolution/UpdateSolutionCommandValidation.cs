using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateSolution
{
    public class UpdateSolutionCommandValidation : AbstractValidator<UpdateSolutionCommand>
    {
        public UpdateSolutionCommandValidation()
        {
            RuleFor(usc => usc.ExerciseId)
                .NotEmpty().WithMessage("Exercise id must be provided");
            
            RuleFor(usc => usc.SolutionId)
                .NotEmpty().WithMessage("Solution id must be provided");
        }
    }
}
