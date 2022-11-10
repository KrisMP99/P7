using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands;
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
                var course = await _unitOfWork.CourseRepository.GetCourse(inviteCode.CourseId);
                
                course.CreateInviteCode(inviteCode);
                await _unitOfWork.CourseRepository.UpdateCourse(course);

                var rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                return rowsAffected;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
