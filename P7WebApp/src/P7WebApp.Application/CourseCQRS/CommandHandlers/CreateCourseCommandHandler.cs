using MediatR;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.Common.Mappings.Profiles;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly ICourseRepository _courseReposity;
        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = CourseMapper.Mapper.Map<Course>(request);
                var rowsAffected = await _courseReposity.AddCourse(course);
                return rowsAffected;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
