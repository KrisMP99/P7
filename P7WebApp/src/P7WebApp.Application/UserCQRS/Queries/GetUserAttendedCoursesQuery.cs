using MediatR;
using P7WebApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.UserCQRS.Queries
{
    public class GetUserAttendedCoursesQuery : IRequest<IEnumerable<CourseOverviewResponse>>
    { 
        public string UserId { get; set; }
    }
}
