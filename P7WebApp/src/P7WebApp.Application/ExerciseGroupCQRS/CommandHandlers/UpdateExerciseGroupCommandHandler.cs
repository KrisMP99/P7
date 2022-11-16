using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.ExerciseGroupCQRS.Commands;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.ExerciseGroupCQRS.CommandHandlers
{
    public class UpdateExerciseGroupCommandHandler : IRequestHandler<UpdateExerciseGroupCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExerciseGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                    course.GetExerciseGroup(request.Id)
                        .EditInformation(newTitle: request.Title, newDescription: request.Description, isVisible: request.IsVisible, newBecomeVisibleAt: request.BecomesVisibleAt, newExerciseGroupNumber: request.ExerciseGroupNumber);

                    int affectedRows = await _unitOfWork.CourseRepository.UpdateCourse(course);

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
