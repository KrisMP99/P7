using MediatR;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly ICourseRepository _courseReposity;

        public CreateCourseCommandHandler(ICourseRepository courseReposity)
        {
            _courseReposity = courseReposity;
        }

        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = CourseMapper.Mapper.Map<Course>(request);
                var rowsAffected = await _courseReposity.CreateCourse(course);
                return rowsAffected;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
