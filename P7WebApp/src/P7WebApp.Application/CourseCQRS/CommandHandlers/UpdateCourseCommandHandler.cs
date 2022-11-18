using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Commands;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroups(request.Id);

                if (course is null)
                {
                    throw new NotFoundException("The course could not be found");
                }
                else
                {
                    course.EditInformation(newTitle: request.Title, newDescription: request.Description, newVisibility: request.IsPrivate);
                    int rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                    return rowsAffected;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
