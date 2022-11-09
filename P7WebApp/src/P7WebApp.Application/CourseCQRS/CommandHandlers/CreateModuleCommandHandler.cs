using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules;
using P7WebApp.Domain.AggregateRoots.ExerciseGroupAggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var module = ExerciseMapper.Mapper.Map<Module>(request);
                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseById(module.ModuleId);

                exercise.AddModule(module);

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
