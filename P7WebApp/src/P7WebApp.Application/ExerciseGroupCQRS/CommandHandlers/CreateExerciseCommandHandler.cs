using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;

namespace P7WebApp.Application.ExerciseGroupCQRS.CommandHandlers
{
    public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateExerciseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = ExerciseMapper.Mapper.Map<Exercise>(request);

                if (exercise is null)
                {
                    throw new Exception("Could not map CreateExerciseCommand to Exercise");
                }
                else
                {
                    //var exerciseGroup = await _unitOfWork.ExerciseGroupRepository.GetExerciseGroupByIdWithExercises(request.ExerciseGroupId);

                    //if (exerciseGroup is null)
                    //{
                    //    throw new NotFoundException($"Could not find an exercise group with Id: {request.ExerciseGroupId}.");
                    //}
                    //else
                    //{
                        await _unitOfWork.ExerciseRepository.CreateExercise(exercise);

                        var result = await _unitOfWork.CommitChangesAsync(cancellationToken);

                        if (result >= 1)
                        {
                            return exercise.Id;
                        }

                        return result;
                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}