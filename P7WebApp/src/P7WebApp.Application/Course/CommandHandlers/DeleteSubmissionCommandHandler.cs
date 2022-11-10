﻿using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class DeleteSubmissionCommandHandler : IRequestHandler<DeleteSubmissionCommand, int>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubmissionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteSubmissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseFromSubmissionId(request.SubmissionId);
                exercise.DeleteSubmission(request.SubmissionId);

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