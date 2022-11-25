using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Commands;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class LeaveCourseCommandHandler : IRequestHandler<LeaveCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public LeaveCourseCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(LeaveCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroups(request.CourseId);
                if (course is not null)
                {
                    course.RemoveAttendee(_currentUserService.UserId);
                    int affectedRows = await _unitOfWork.CommitChangesAsync(cancellationToken);
                    return affectedRows;
                }
                else
                {
                    throw new NotFoundException("Could not find course");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
