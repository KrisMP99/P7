using MediatR;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetListOfCoursesQueryHandler : IRequestHandler<GetListOfCoursesQuery, IEnumerable<CourseResponse>>
    {
        ICourseRepository _courseRepository;

        public GetListOfCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async  Task<IEnumerable<CourseResponse>> Handle(GetListOfCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courses = await _courseRepository.GetListOfCourses(request.Amount);
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
