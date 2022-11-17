using MediatR;
using P7WebApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Queries
{
    public class GetOwnedCoursesQuery : IRequest<IEnumerable<CourseResponse>>
    {
        public GetOwnedCoursesQuery(string userId)
        {
            this.userId = userId;
        }

        public string userId { get; set; }
    }
}
