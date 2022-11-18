using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.Responses;
using P7WebApp.Application.UserCQRS.Queries;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.UserCQRS.QueryHandlers
{
    public class GetUserAttendedCoursesQueryHandler : IRequestHandler<GetUserAttendedCoursesQuery, IEnumerable<CourseOverviewResponse>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetUserAttendedCoursesQueryHandler(ICourseRepository courseRepository, ICurrentUserService currentUserService)
        {
            _courseRepository = courseRepository;
            _currentUserService = currentUserService;
        }
        // TODO: Implement correctly
        public async Task<IEnumerable<CourseOverviewResponse>> Handle(GetUserAttendedCoursesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId is null) throw new Exception("User not found.");

            var courses = await _courseRepository.GetAttendedCourses(22);
            var result = CourseMapper.Mapper.Map<IEnumerable<CourseOverviewResponse>>(courses);

            return result;

        }
    }
}
