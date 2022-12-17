using MediatR;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ProfileCQRS.Queries;
using P7WebApp.Application.Responses;
using System.Diagnostics;

namespace P7WebApp.Application.ProfileCQRS.QueryHandlers
{
    public class GetProfileAttendedCoursesQueryHandler : IRequestHandler<GetProfileAttendedCoursesQuery, IEnumerable<CourseOverviewResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetProfileAttendedCoursesQueryHandler> _logger;

        public GetProfileAttendedCoursesQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ILogger<GetProfileAttendedCoursesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<IEnumerable<CourseOverviewResponse>> Handle(GetProfileAttendedCoursesQuery request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation($"GetProfileAttendedCoursesQueryHandler.Handle() began");

            try
            {
                var userId = _currentUserService.UserId;

                if (userId is null)
                {
                    sw.Stop();
                    _logger.LogInformation($"GetProfileAttendedCoursesQueryHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                    throw new NotFoundException("User not found.");
                }

                var courses = await _unitOfWork.CourseRepository.GetAttendedCourses(userId);

                if (courses is null)
                {
                    sw.Stop();
                    _logger.LogInformation($"GetProfileAttendedCoursesQueryHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                    throw new NotFoundException("Could not find the course with the given user id.");
                }

                var result = CourseMapper.Mapper.Map<IEnumerable<CourseOverviewResponse>>(courses);

                if (result is null)
                {
                    sw.Stop();
                    _logger.LogInformation($"GetProfileAttendedCoursesQueryHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                    throw new Exception("Could not map the course to the course overview object.");
                }

                sw.Stop();
                _logger.LogInformation($"GetProfileAttendedCoursesQueryHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogWarning($"GetProfileAttendedCoursesQueryHandler.Handle() failed with message {ex.Message} after {sw.ElapsedMilliseconds}");
                throw;
            }
        }
    }
}
