using MediatR;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateCourseCommand : IRequest<int>
    {
        public CreateCourseCommand(string title, string description, bool isPrivate, List<Exercise> exercise)
        {
            Title = title;
            Description = description;
            IsPrivate = isPrivate;
            Exercise = exercise;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public List<Exercise> Exercise { get; set; }
    }
}
