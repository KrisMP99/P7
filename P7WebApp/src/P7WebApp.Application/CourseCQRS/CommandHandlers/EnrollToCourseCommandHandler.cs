using MediatR;
using P7WebApp.Application.Common.Exceptions;
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
                // The number specifies the rollId. For now there is only attendee and owner.
                // Therefore the roleId is 1 -> Attendee
                //var attendee = new Attendee(_currentUserService.UserId, request.CourseId, 1);
                //var course = await _unitOfWork.CourseRepository.GetCourseWithAttendees(request.CourseId);
                //if (course is not null)
                //{
                //    course.AddAttendee(attendee);
                //    var affectedRows = await _unitOfWork.CommitChangesAsync(cancellationToken);
                //    return affectedRows;
                //}
                //else
                //{
                //    throw new NotFoundException("Could not find course with specified ID");
                //}
                throw new NotImplementedException();

            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
