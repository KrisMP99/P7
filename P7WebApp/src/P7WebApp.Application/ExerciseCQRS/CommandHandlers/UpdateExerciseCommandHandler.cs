using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.ExerciseCQRS.Commands;
using P7WebApp.Domain.Repositories;

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
                var course = await _unitOfWork.CourseRepository.GetCourseFromExerciseGroupId(request.ExerciseGroupId);
                course.GetExerciseGroup(request.ExerciseGroupId)
                    .GetExercise(request.Id)
                    .UpdateExerciseInformation(newTitle: request.Title,
                                               visibility: request.IsVisible,
                                               exerciseNumber: request.ExerciseNumber,
                                               newStartDate: request.StartDate,
                                               newEndDate: request.EndDate);

                var affectedRows = await _unitOfWork.CourseRepository.UpdateCourse(course);

                return affectedRows;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
