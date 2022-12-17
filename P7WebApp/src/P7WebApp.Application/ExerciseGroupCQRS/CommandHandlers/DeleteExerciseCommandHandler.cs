using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.DeleteExercise;

namespace P7WebApp.Application.ExerciseGroupCQRS.CommandHandlers
{
    public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExerciseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exerciseGroup = await _unitOfWork.ExerciseGroupRepository.GetExerciseGroupByIdWithExercises(request.ExerciseGroupId);

                if (exerciseGroup is null)
                {
                    throw new NotFoundException($"Could not find an exercise group with Id: {request.ExerciseGroupId} with exercise Id: {request.Id}.");
                }

                exerciseGroup.RemoveExerciseById(request.Id);

                int rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}