using MediatR;
using P7WebApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Queries
{
    public class GetListOfCoursesQuery : IRequest<IEnumerable<CourseResponse>>
    {

        public GetListOfCoursesQuery(int amount)
        {
            Amount = amount;
        }
        public int Amount { get; set; }
    }
}
