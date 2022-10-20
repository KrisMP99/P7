using MediatR;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, int>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateExerciseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<int> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetCourseFromExerciseGroupId(request.ExerciseGroupId);
                course.GetGroupExercise(request.ExerciseGroupId)
                    .GetExercise(request.Id)
                    .UpdateInformation(newTitle: request.Title, visibility: request.IsVisible, exerciseNumber: request.ExerciseNumber, newStartDate: request.StartDate, newEndDate: request.EndDate);

                var affectedRows = await _courseRepository.UpdateCourse(course);

                return affectedRows;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
