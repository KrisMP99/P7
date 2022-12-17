using MediatR;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Commands.CreateCourse;
using System.Diagnostics;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<CreateCourseCommandHandler> _logger;

        public CreateCourseCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ILogger<CreateCourseCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("CreateCourseCommandHandler.Handle() began");

            try
            {
                var profile = await _unitOfWork.ProfileRepository.GetProfileByUserId(_currentUserService.UserId);

                if(profile is null)
                {
                    sw.Stop();
                    _logger.LogInformation($"CreateCourseCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");
                    throw new NotFoundException($"Could not find a profile with user id {profile.Id}.");
                }

                var course = profile.CreateCourse(request.Title, request.Description, request.IsPrivate);

                if(course is null)
                {
                    sw.Stop();
                    _logger.LogInformation($"CreateCourseCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");
                    throw new Exception($"Could not create the course {request.Title}.");
                }

                await _unitOfWork.CourseRepository.CreateCourse(course);

                var affectedRows = await _unitOfWork.CommitChangesAsync(cancellationToken);

                sw.Stop();
                _logger.LogInformation($"CreateCourseCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                return affectedRows;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogWarning($"CreateCourseCommandHandler.Handle() failed with message {ex.Message} after {sw.ElapsedMilliseconds}");
                throw;
            }
        }
    }
}
