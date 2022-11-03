using MediatR;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
    {
        private readonly ICourseRepository _courseRepository;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int courseId = CourseMapper.Mapper.Map<int>(request);
                bool deleted = await _courseRepository.DeleteCourse(courseId);
                return deleted;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
