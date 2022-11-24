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
                .NotEmpty().WithMessage("Exercise id must be provided")
                .NotNull().WithMessage("Exercise id cannot be null")
                .GreaterThan(0).WithMessage("Exercise id cannot be negative");
            
            RuleFor(usc => usc.SolutionId)
                .NotEmpty().WithMessage("Solution id must be provided")
                .NotNull().WithMessage("Solution id cannot be null")
                .GreaterThan(0).WithMessage("Solution id cannot be negative");
        }
    }
}
