using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.QueryHandlers
{
    public class GetCourseFromInviteCodeQueryHandler : IRequestHandler<GetCourseFromInviteCodeQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCourseFromInviteCodeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetCourseFromInviteCodeQuery request, CancellationToken cancellationToken)
        {
            var id = await _unitOfWork.CourseRepository.GetCourseFromInviteCode(request.Code);

            return id;
        }
    }
}
