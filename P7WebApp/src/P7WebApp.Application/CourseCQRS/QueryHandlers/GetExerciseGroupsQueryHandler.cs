using MediatR;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetExerciseGroupsQueryHandler : IRequestHandler<GetExerciseGroupsQuery, IEnumerable<ExerciseGroupsResponse>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetExerciseGroupsQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<ExerciseGroupsResponse>> Handle(GetExerciseGroupsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var exerciseGroup = await _courseRepository.GetExerciseGroups(request.Id);
                var response = CourseMapper.Mapper.Map<IEnumerable<ExerciseGroupsResponse>>(exerciseGroup);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
