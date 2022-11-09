using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class DeleteCourseCommand : IRequest<int>
    {

        public DeleteCourseCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
