using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCourseQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<CourseResponse> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroupsAndExercisesAndAttendess(request.Id);
                
                if (course is null)
                {
                    throw new NotFoundException("Course could not be found");
                }

                var result = CourseMapper.Mapper.Map<CourseResponse>(course);

                if (result is null)
                {
                    throw new Exception("Could not map course to course response.");
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}