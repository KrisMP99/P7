using AutoMapper;
using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise;
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
                var exerciseGroup = await _unitOfWork.ExerciseGroupRepository.GetExerciseGroupByIdWithExercises(request.ExerciseGroupId);
                exerciseGroup.AddExercise(exercise);

                var result = await _unitOfWork.CommitChangesAsync(cancellationToken); 

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
