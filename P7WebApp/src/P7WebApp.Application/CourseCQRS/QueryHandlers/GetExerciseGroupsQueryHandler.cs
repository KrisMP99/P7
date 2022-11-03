using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetExerciseGroupsQueryHandler : IRequestHandler<GetExerciseGroupsQuery, IEnumerable<ExerciseGroupResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetExerciseGroupsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ExerciseGroupResponse>> Handle(GetExerciseGroupsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var exerciseGroup = await _unitOfWork.ExerciseGroupRepository.GetExerciseGroupsByCourseId(request.Id);
                var response = CourseMapper.Mapper.Map<IEnumerable<ExerciseGroupResponse>>(exerciseGroup);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
