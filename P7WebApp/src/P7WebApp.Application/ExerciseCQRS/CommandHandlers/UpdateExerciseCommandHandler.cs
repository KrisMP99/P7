﻿using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.ExerciseCQRS.Commands;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.ExerciseCQRS.CommandHandlers
{
    public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExerciseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                var exerciseGroup = await _unitOfWork.ExerciseGroupRepository.GetExerciseGroupByIdWithExercises(request.ExerciseGroupId);
                exerciseGroup
                    .GetExercise(request.Id)
                    .EditInformation(newTitle: request.Title,
                                               newIsVisible: request.IsVisible,
                                               newExerciseNumber: request.ExerciseNumber,
                                               newStartDate: request.StartDate,
                                               newEndDate: request.EndDate,
                                               newLayoutId: request.LayoutId);

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
