using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Interfaces.Identity;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetCreatedCoursesHandler : IRequestHandler<GetOwnedCoursesQuery,IEnumerable<CourseOverviewResponse>>
    {
		private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetCreatedCoursesHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<CourseOverviewResponse>> Handle(GetOwnedCoursesQuery request, CancellationToken cancellationToken)
        {
			try
			{
                var courses = await _unitOfWork.CourseRepository.GetCreatedCourses(request.userId);
                var response = CourseMapper.Mapper.Map<IEnumerable<CourseOverviewResponse>>(courses);
                var fullName = _currentUserService.FirstName + " " + _currentUserService.LastName;
                var responseWithName = CourseMapper.Mapper.Map<IEnumerable<CourseOverviewResponse>>(fullName);

                return responseWithName;
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
