using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands.CreateExerciseGroup;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateExerciseGroupCommandHandler : IRequestHandler<CreateExerciseGroupCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateExerciseGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateExerciseGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exerciseGroup = CourseMapper.Mapper.Map<ExerciseGroup>(request);
                var course = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroups(request.CourseId);

                course.AddExerciseGroup(exerciseGroup);

                var rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return rowsAffected;
            }
            catch(Exception)
            {
                throw new Exception($"Could not create exercise group '{request.Title}' for exercise with Id: {request.CourseId}.");
            }
        }
    }
}
