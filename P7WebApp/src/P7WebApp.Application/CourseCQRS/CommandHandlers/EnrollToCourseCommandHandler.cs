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
                var attendee = new Attendee(_currentUserService.UserId, request.CourseId);
                var course = await _unitOfWork.CourseRepository.GetCourseWithAttendees(request.CourseId);

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
