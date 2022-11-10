using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Commands;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand, int>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
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
