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
                .NotEmpty().WithMessage("Exercise id must be provided")
                .NotNull().WithMessage("Exercise id cannot be null")
                .GreaterThan(0).WithMessage("Exericse id cannot be negative");

            RuleFor(dsc => dsc.SolutionId)
                .NotEmpty().WithMessage("Solution id must be provided")
                .NotNull().WithMessage("Solution id cannot be null")
                .GreaterThan(0).WithMessage("Solution id cannot be negative");
        }
    }
}
