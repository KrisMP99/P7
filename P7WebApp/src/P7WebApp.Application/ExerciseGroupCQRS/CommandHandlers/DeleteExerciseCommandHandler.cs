using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ExerciseGroupCQRS.Commands;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.CommandHandlers
{
    public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExerciseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.ExerciseGroupRepository.DeleteExercise(request.id);

                if (result != 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Could not delete exercise");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
