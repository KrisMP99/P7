using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.ExerciseCQRS.Commands.DeleteSubmission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.CommandHandlers
{
    public class DeleteSubmissionCommandHandler : IRequestHandler<DeleteSubmissionCommand, int>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubmissionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteSubmissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseWithSubmissionsById(request.SubmissionId);
                exercise.RemoveSubmissionById(request.SubmissionId);

                var rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return rowsAffected;
            }
            catch (Exception)
            {
                throw new NotFoundException($"Could not delete submission Id: {request.SubmissionId} for exercise with Id: {request.ExerciseId}.");
            }
        }
    }
}
