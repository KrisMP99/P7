using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands.DeleteCourse;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.Common.Exceptions;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCourseCommandHandler> _logger;

        public DeleteCourseCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCourseCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("DeleteCourseCommandHandler.Handle() began");

            try
            {
                await _unitOfWork.CourseRepository.DeleteCourse(request.Id);
                int rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                if (rowsAffected == 0)
                {
                    sw.Stop();
                    _logger.LogInformation($"DeleteCourseCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");
                    throw new NotFoundException($"Could not find a course witht the specified Id {request.Id}");
                }

                sw.Stop();
                _logger.LogInformation($"DeleteCourseCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                return rowsAffected;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogWarning($"DeleteCourseCommandHandler.Handle() failed with message {ex.Message} after {sw.ElapsedMilliseconds}");
                throw;
            }
        }
    }
}