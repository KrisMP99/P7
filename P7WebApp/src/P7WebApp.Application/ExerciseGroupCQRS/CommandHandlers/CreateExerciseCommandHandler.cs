using MediatR;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using System.Diagnostics;

namespace P7WebApp.Application.ExerciseGroupCQRS.CommandHandlers
{
    public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateExerciseCommandHandler> _logger;

        public CreateExerciseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("CreateExerciseCommandHandler.Handle() began");

            try
            {
                var exercise = ExerciseMapper.Mapper.Map<Exercise>(request);

                if (exercise is null)
                {
                    sw.Stop();
                    _logger.LogInformation($"CreateExerciseCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");
                    throw new Exception("Could not map CreateExerciseCommand to Exercise");
                }
                else
                {
                    //var exerciseGroup = await _unitOfWork.ExerciseGroupRepository.GetExerciseGroupByIdWithExercises(request.ExerciseGroupId);

                    //if (exerciseGroup is null)
                    //{
                    //    throw new NotFoundException($"Could not find an exercise group with Id: {request.ExerciseGroupId}.");
                    //}
                    //else
                    //{
                        await _unitOfWork.ExerciseRepository.CreateExercise(exercise);

                        var result = await _unitOfWork.CommitChangesAsync(cancellationToken);

                        if (result >= 1)
                        {
                            sw.Stop();
                            _logger.LogInformation($"CreateExerciseCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");
                            return exercise.Id;
                        }


                    sw.Stop();
                    _logger.LogInformation($"CreateExerciseCommandHandler.Handle() finished with time in milliseconds: {sw.ElapsedMilliseconds}");

                    return result;
                    //}
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogWarning($"CreateExerciseCommandHandler.Handle() failed with message {ex.Message} after {sw.ElapsedMilliseconds}");
                throw;
            }
        }
    }
}