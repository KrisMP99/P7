using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Repositories;

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
            var entity = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroups(request.Id);
            var result = CourseMapper.Mapper.Map<CourseResponse>(entity);
            if (result == null)
            {
                throw new Exception("issue with mapper");
            }
            return result;
        }
    }
}
