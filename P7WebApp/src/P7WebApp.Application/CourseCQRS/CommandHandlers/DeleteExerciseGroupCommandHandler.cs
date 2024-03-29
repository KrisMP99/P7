﻿using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Commands.DeleteExerciseGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class DeleteExerciseGroupCommandHandler : IRequestHandler<DeleteExerciseGroupCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExerciseGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteExerciseGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroups(request.CourseId);
                course.RemoveExerciseGroup(request.ExerciseGroupId);

                var rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return rowsAffected;
                
            }
            catch(Exception)
            {
                throw new Exception($"Could not delete exercise group Id: {request.ExerciseGroupId} in course Id: {request.CourseId}.");
            }
        }
    }
}
