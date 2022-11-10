﻿using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateSolutionCommandHandler : IRequestHandler<CreateSolutionCommand, int>
    {

        private readonly IUnitOfWork _unitOfWork;

        public CreateSolutionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateSolutionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var solution = ExerciseMapper.Mapper.Map<Solution>(request);
                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseById(request.SolutionId);

                exercise.AddSolution(solution);

                var rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return rowsAffected;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}