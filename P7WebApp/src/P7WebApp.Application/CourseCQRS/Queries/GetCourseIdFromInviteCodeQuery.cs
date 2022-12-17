using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Queries
{
    public class GetCourseIdFromInviteCodeQuery : IRequest<int>
    {
        public GetCourseIdFromInviteCodeQuery(int code)
        {
            Code = code;
        }

        public int Code { get; set; }

    }
}
