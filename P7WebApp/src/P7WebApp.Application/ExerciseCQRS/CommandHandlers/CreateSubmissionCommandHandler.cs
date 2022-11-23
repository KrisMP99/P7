using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.ExerciseCQRS.Commands.CreateSubmission;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;

namespace P7WebApp.Application.ExerciseCQRS.CommandHandlers
{
    public class CreateSubmissionCommandHandler : IRequestHandler<CreateSubmissionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubmissionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var submission = ExerciseMapper.Mapper.Map<Submission>(request);
                var exercise = await _unitOfWork.ExerciseRepository.GetExerciseWithSubmissionsById(request.ExerciseId);

                exercise.AddSubmission(submission);

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
