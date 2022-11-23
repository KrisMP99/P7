using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateSolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.CommandHandlers
{
    public class UpdateSolutionCommandHandler : IRequestHandler<UpdateSolutionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSolutionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateSolutionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseWithSolutionsById(request.ExerciseId);

                if (exercise is not null)
                {
                    exercise.GetSolution(request.SolutionId)
                        .EditInformation(isVisible: request.IsVisible,
                                         visibleFromDate: request.VisibleFromDate);

                    var affectedRows = await _unitOfWork.CommitChangesAsync(cancellationToken);
                    return affectedRows;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}