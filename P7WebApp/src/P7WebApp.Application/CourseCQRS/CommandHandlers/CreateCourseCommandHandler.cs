using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = CourseMapper.Mapper.Map<Course>(request);

                await _unitOfWork.CourseRepository.CreateCourse(course);

                var result = await _unitOfWork.CommitChangesAsync(cancellationToken);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
