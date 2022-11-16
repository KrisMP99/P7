using MediatR;
using P7WebApp.Application.ExerciseCQRS.Commands;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.ExerciseCQRS.CommandHandlers
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
                course.GetExerciseGroup(request.ExerciseGroupId)
                    .GetExercise(request.Id)
                    .UpdateExerciseInformation(newTitle: request.Title,
                                               visibility: request.IsVisible,
                                               exerciseNumber: request.ExerciseNumber,
                                               newStartDate: request.StartDate,
                                               newEndDate: request.EndDate);

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
