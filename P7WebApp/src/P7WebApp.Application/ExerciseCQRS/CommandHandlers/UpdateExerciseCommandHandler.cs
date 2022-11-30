using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;

namespace P7WebApp.Application.ExerciseCQRS.CommandHandlers
{
    public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExerciseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseWithModulesById(request.Id);

                if (exercise is null)
                {
                    throw new NotFoundException($"Could not update exercise '{request.Title}' with Id: {request.Id}.");
                }
                else
                {
                    var modules = ExerciseMapper.Mapper.Map<List<Module>>(request.Modules);

                    exercise
                        .EditInformation(newTitle: request.Title,
                                         newIsVisible: request.IsVisible,
                                         newExerciseNumber: request.ExerciseNumber,
                                         newStartDate: request.StartDate,
                                         newEndDate: request.EndDate,
                                         newLayoutId: request.LayoutId,
                                         newModules: modules);


                    var affectedRows = await _unitOfWork.CommitChangesAsync(cancellationToken);

                    return affectedRows;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}