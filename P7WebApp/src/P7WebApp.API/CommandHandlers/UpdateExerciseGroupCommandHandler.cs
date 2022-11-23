﻿using MediatR;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.ExerciseGroupCQRS.Commands;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.ExerciseGroupCQRS.CommandHandlers
{
    public class UpdateExerciseGroupCommandHandler : IRequestHandler<UpdateExerciseGroupCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExerciseGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateExerciseGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroups(request.CourseId);

                if (course is null)
                {
                    throw new Exception("Could not find the exercise");
                }
                else
                {
                    course.GetExerciseGroup(request.Id)
                        .EditInformation(newTitle: request.Title, newDescription: request.Description, isVisible: request.IsVisible, newBecomeVisibleAt: request.BecomesVisibleAt, newExerciseGroupNumber: request.ExerciseGroupNumber);
                    int rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
