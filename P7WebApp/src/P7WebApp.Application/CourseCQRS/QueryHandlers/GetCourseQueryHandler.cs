using MediatR;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseResponse>

    {
        private readonly ICourseRepository courseRepository;
        public GetCourseQueryHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<CourseResponse> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var entity = await courseRepository.GetCourse(request.Id);
            var result = CourseMapper.Mapper.Map<CourseResponse>(entity);
            if (result == null)
            {
                throw new Exception("issue with mapper");
            }
            return result;
        }
    }
}
