using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Commands;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class DeleteExerciseModuleCommandHandler : IRequestHandler<DeleteExerciseModuleCommand, int>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteExerciseModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteExerciseModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseFromModuleId(request.ModuleId);
                exercise.DeleteModule(request.ModuleId);

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
