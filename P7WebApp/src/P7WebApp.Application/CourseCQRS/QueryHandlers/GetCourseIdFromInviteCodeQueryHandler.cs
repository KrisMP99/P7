using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Queries;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetCourseIdFromInviteCodeQueryHandler : IRequestHandler<GetCourseIdFromInviteCodeQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCourseIdFromInviteCodeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetCourseIdFromInviteCodeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var id = await _unitOfWork.CourseRepository.GetCourseIdFromInviteCode(request.Code);

                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
