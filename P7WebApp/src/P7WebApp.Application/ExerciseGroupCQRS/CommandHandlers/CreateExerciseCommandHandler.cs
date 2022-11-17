using AutoMapper;
using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ExerciseGroupCQRS.Commands;
using P7WebApp.Domain.Aggregates.CourseAggregate;
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
                var exerciseId = await _unitOfWork.ExerciseGroupRepository.CreateExercise(exercise);

                if (exerciseId != 0)
                {
                    return exerciseId;
                }
                else
                {
                    throw new Exception("Could not create exercise");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
