using MediatR;
using P7WebApp.Application.Common.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        public GetListOfCoursesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<IEnumerable<CourseResponse>> Handle(GetListOfCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courses = await _unitOfWork.CourseRepository.GetListOfCourses();
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