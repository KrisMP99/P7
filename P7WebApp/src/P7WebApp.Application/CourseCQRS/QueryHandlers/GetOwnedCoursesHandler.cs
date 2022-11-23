using MediatR;
using P7WebApp.Application.Common.Interfaces;
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
    public class GetCreatedCoursesHandler : IRequestHandler<GetOwnedCoursesQuery,IEnumerable<CourseResponse>>
    {
		private readonly IUnitOfWork _unitOfWork;

        public GetCreatedCoursesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CourseResponse>> Handle(GetOwnedCoursesQuery request, CancellationToken cancellationToken)
        {
			try
			{
                var courses = await _unitOfWork.CourseRepository.GetCreatedCourses(request.userId);
                var response = CourseMapper.Mapper.Map<IEnumerable<CourseResponse>>(courses);
                return response;
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
