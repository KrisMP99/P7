using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ProfileCQRS.Queries;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.ProfileCQRS.QueryHandlers
{
    public class GetProfileAttendedCoursesQueryHandler : IRequestHandler<GetProfileAttendedCoursesQuery, IEnumerable<CourseOverviewResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetProfileAttendedCoursesQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<CourseOverviewResponse>> Handle(GetProfileAttendedCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.ProfileId;

                if (userId == 0)
                {
                    throw new NotFoundException("User not found.");
                }

                var courses = await _unitOfWork.CourseRepository.GetAttendedCourses(userId);

                if (courses is null)
                {
                    throw new NotFoundException("Could not find the course with the given user id.");
                }

                var result = CourseMapper.Mapper.Map<IEnumerable<CourseOverviewResponse>>(courses);

                if (result is null)
                {
                    throw new Exception("Could not map the course to the course overview object.");
                }

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
