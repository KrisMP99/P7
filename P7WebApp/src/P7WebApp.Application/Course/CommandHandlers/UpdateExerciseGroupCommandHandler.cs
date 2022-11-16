using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class UpdateExerciseGroupCommandHandler : IRequestHandler<UpdateExerciseGroupCommand, int>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateExerciseGroupCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<int> Handle(UpdateExerciseGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetCourse(request.CourseId);

                if (course is null)
                {
                    throw new NotFoundException("Could not find the exercise");
                }
                else
                {
                    course.GetExerciseGroup(request.ExerciseGroupId)
                        .EditInformation(newTitle: request.Title, newDescription: request.Description, isVisible: request.IsVisible, newBecomeVisibleAt: request.BecomesVisibleAt, newExerciseGroupNumber: request.ExerciseGroupNumber);

                    int affectedRows = await _courseRepository.UpdateCourse(course);

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
