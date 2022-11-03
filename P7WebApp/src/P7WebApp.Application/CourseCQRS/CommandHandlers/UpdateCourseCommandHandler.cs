using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, int>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<int> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetCourse(request.Id);

                if (course is null)
                {
                    throw new NotFoundException("The course could not be found");
                }
                else
                {
                    course.EditInformation(newTitle: request.Title, newDescription: request.Description, newVisibility: request.IsPrivate);
                    int rowsAffected = await _courseRepository.UpdateCourse(course);

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
