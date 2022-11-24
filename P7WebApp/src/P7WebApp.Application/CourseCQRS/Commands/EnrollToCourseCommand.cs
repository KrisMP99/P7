using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class EnrollToCourseCommand : IRequest<int>
    {
        public EnrollToCourseCommand(int courseId)
        {
            CourseId = courseId;
        }

        public int CourseId { get; set; }
    }
}
