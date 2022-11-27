using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class EnrollToCourseCommandHandler : IRequestHandler<EnrollToCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public EnrollToCourseCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(EnrollToCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _unitOfWork.ProfileRepository.GetProfileByUserId(_currentUserService.UserId);

                var course = await _unitOfWork.CourseRepository.GetCourseWithAttendeesAndDefaultCourseRoles(request.CourseId);

                var defaultRole = course.CourseRoles.Where(role => role.IsDefaultRole).FirstOrDefault();

                var attendee = new Attendee(
                    courseId: request.CourseId,
                    courseRoleId: defaultRole.Id,
                    profileId: profile.Id);

                course.AddAttendee(attendee);

                var affectedRows = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return affectedRows;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
