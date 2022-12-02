using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands.CreateCourse;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;


        public CreateCourseCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _unitOfWork.ProfileRepository.GetProfileByUserId(_currentUserService.UserId);

                if(profile is null)
                {
                    throw new NotFoundException($"Could not find a profile with user id {profile.Id}.");
                }

                var course = profile.CreateCourse(request.Title, request.Description, request.IsPrivate);

                if(course is null)
                {
                    throw new Exception($"Could not create the course {request.Title}.");
                }

                await _unitOfWork.CourseRepository.CreateCourse(course);

                var affectedRows = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return affectedRows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
