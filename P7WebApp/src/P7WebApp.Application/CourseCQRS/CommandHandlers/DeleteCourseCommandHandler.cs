using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Commands;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.CourseRepository.DeleteCourse(request.Id);
                int rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);
                return rowsAffected;
            }
            catch (Exception)
            { 
                throw;
            }
        }
    }
}