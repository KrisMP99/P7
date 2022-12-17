using MediatR;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using System.Diagnostics;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetCreatedCoursesQueryHandler : IRequestHandler<GetCreatedCoursesQuery, IEnumerable<CourseOverviewResponse>>
    {
		private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetCreatedCoursesQueryHandler> _logger;

        public GetCreatedCoursesQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ILogger<GetCreatedCoursesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<IEnumerable<CourseOverviewResponse>> Handle(GetCreatedCoursesQuery request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("GetCreatedCoursesQueryHandler.Handle() began");
            
            try
			{
                var userId = _currentUserService.UserId;
                var courses = await _unitOfWork.CourseRepository.GetCreatedCourses(userId);
                var response = CourseMapper.Mapper.Map<IEnumerable<CourseOverviewResponse>>(courses);
                var fullName = _currentUserService.FullName;

                // TODO: Convince Jonas to change the frontend, or do this in some other way
                foreach (var course in response)
                {
                    course.OwnerName = fullName;
                }

                sw.Stop();
                _logger.LogInformation($"GetCreatedCoursesQueryHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                return response;
			}
			catch (Exception ex)
			{
                sw.Stop();
                _logger.LogWarning($"GetCreatedCoursesQueryHandler.Handle() failed with message {ex.Message} after {sw.ElapsedMilliseconds}");
                throw;
            }
        }
    }
}
