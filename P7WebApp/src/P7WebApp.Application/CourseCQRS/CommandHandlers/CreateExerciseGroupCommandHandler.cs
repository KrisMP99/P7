using MediatR;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands.CreateExerciseGroup;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateExerciseGroupCommandHandler : IRequestHandler<CreateExerciseGroupCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateExerciseGroupCommandHandler> _logger;

        public CreateExerciseGroupCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateExerciseGroupCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> Handle(CreateExerciseGroupCommand request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("CreateExerciseGroupCommand.Handle() began");

            try
            {
                var exerciseGroup = CourseMapper.Mapper.Map<ExerciseGroup>(request);
                //var course = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroups(request.CourseId);

                //course.AddExerciseGroup(exerciseGroup);
                await _unitOfWork.ExerciseGroupRepository.CreateExerciseGroupAsync(exerciseGroup);
                var rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                sw.Stop();
                _logger.LogInformation($"CreateExerciseGroupCommand.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                return rowsAffected;
            }
            catch(Exception ex)
            {
                sw.Stop();
                _logger.LogWarning($"CreateExerciseGroupCommand.Handle() failed with message {ex.Message} after {sw.ElapsedMilliseconds}");
                throw new Exception($"Could not create exercise group '{request.Title}' for exercise with Id: {request.CourseId}.");
            }
        }
    }
}
