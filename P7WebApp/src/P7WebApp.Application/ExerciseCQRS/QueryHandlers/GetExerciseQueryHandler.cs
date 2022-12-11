using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ExerciseCQRS.Queries.Get;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.ExerciseCQRS.QueryHandlers
{
    public class GetExerciseQueryHandler : IRequestHandler<GetExerciseQuery, ExerciseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetExerciseQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<ExerciseResponse> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseWithAttendees(request.CourseId) ?? throw new Exception("Could not find the specified course"); ;
                var profile = await _unitOfWork.ProfileRepository.GetProfileByUserId(_currentUserService.UserId) ?? throw new NotFoundException("User could not be found");
                
                // check if user has access to the exercise
                if (course.OwnerId != profile.Id && !course.Attendees.Any(a => a.ProfileId == profile.Id))
                {
                    throw new Exception("User does not have access to the exercise");
                }

                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseWithModules(request.ExerciseGroupId, request.ExerciseId);

                if (exercise is null)
                {
                    throw new NotFoundException($"Could not find an exercise with id {request.ExerciseId}");
                }

                var response = ExerciseMapper.Mapper.Map<ExerciseResponse>(exercise);

                if (response is null)
                {
                    throw new Exception("Issue mapping an exercise to exerciseResponse");
                }

                return response;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}