using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.ExerciseCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.CommandHandlers
{
    public class DeleteSolutionCommandHandler : IRequestHandler<DeleteSolutionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSolutionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteSolutionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseWithSolutionsById(request.ExerciseId);
                exercise.RemoveSolutionById(request.SolutionId);

                var rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
