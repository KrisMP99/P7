using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.Responses;
using P7WebApp.Application.UserCQRS.Queries;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.UserCQRS.QueryHandlers
{
    public class GetUserCreatedCoursesQueryHandler : IRequestHandler<GetUserCreatedCoursesQuery, IEnumerable<CourseOverviewResponse>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetUserCreatedCoursesQueryHandler(ICourseRepository courseRepository, ICurrentUserService currentUserService)
        {
            _courseRepository = courseRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<CourseOverviewResponse>> Handle(GetUserCreatedCoursesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId is null) throw new Exception("User not found");

            var courses = await _courseRepository.GetUsersCreatedCourses(userId);
            var result = CourseMapper.Mapper.Map<IEnumerable<CourseOverviewResponse>>(courses);

            if (result is null)
            {
                throw new Exception("Could not map courses to course overview");
            }

            return result;
        }
    }
}
