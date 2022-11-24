using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands.CreateInviteCode;
using P7WebApp.Domain.Aggregates.CourseAggregate;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class CreateInviteCodeCommandHandler : IRequestHandler<CreateInviteCodeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateInviteCodeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateInviteCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var inviteCode = CourseMapper.Mapper.Map<InviteCode>(request);
                var course = await _unitOfWork.CourseRepository.GetCourseWithExerciseGroups(inviteCode.CourseId);
                if (course.InviteCode is null)
                {
                    course.CreateInviteCode(inviteCode);
                    await _unitOfWork.CommitChangesAsync(cancellationToken);
                }

                return course.InviteCode.Code;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
