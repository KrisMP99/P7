using MediatR;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using System.Diagnostics;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCourseQueryHandler> _logger;

        public GetCourseQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCourseQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CourseResponse> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("GetCourseQueryHandler.Handle() began");

            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroupsAndExercisesAndAttendess(request.Id);
                
                if (course is null)
                {
                    sw.Stop();
                    _logger.LogInformation($"GetCourseQueryHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");
                    throw new NotFoundException("Course could not be found");
                }

                var result = CourseMapper.Mapper.Map<CourseResponse>(course);

                if (result is null)
                {
                    sw.Stop();
                    _logger.LogInformation($"GetCourseQueryHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");
                    throw new Exception("Could not map course to course response.");
                }

                sw.Stop();
                _logger.LogInformation($"GetCourseQueryHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogWarning($"GetCourseQueryHandler.Handle() failed with message {ex.Message} after {sw.ElapsedMilliseconds}");
                throw;
            }
        }
    }
}