using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.AggregateRoots.ExerciseGroupAggregateRoot;
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
                var course = await _unitOfWork.CourseRepository.GetCourse(exerciseGroup.Id);

                course.CreateExerciseGroup(exerciseGroup);
                await _unitOfWork.CourseRepository.UpdateCourse(course);

                var rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return rowsAffected;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
