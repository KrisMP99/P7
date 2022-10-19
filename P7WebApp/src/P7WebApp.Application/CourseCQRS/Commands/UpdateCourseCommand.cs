using MediatR;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class UpdateCourseCommand : IRequest<int>
    {
        public UpdateCourseCommand(string title, string description, bool isPrivate)
        {
            Title = title;
            Description = description;
            IsPrivate = isPrivate;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}