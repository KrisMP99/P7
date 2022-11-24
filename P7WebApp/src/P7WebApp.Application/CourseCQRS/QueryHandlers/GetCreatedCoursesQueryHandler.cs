using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetCreatedCoursesQueryHandler : IRequestHandler<GetCreatedCoursesQuery, IEnumerable<CourseOverviewResponse>>
    {
		private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetCreatedCoursesQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<CourseOverviewResponse>> Handle(GetCreatedCoursesQuery request, CancellationToken cancellationToken)
        {
			try
			{
                var userId = _currentUserService.UserId;
                var courses = await _unitOfWork.CourseRepository.GetCreatedCourses(userId);
                var response = CourseMapper.Mapper.Map<IEnumerable<CourseOverviewResponse>>(courses);
                var fullName = _currentUserService.FullName;

                // TODO: Convince Jonas to change the frontend, or do this in some other way
                foreach (var course in response)
                {
                    course.OwnerName = fullName;
                }
                
                return response;
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
