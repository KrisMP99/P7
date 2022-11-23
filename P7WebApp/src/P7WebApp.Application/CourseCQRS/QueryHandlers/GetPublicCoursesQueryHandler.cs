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
    public class GetPublicCoursesQueryHandler : IRequestHandler<GetPublicCoursesQuery, IEnumerable<CourseOverviewResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPublicCoursesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CourseOverviewResponse>> Handle(GetPublicCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courses = await _unitOfWork.CourseRepository.GetPublicCourses();
                var response = CourseMapper.Mapper.Map<IEnumerable<CourseOverviewResponse>>(courses);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
