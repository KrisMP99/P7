using MediatR;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateCourseCommand : IRequest<int>
    {
        public CreateCourseCommand(string title, string description, bool isPrivate)
        {
            Title = title;
            Description = description;
            IsPrivate = isPrivate;
        }

        //public string? CreatedById { get; set; }
        //public string? LastModifiedById { get; set; }
        //public ApplicationUser? CreatedBy { get; set; }
        //public ApplicationUser? LastModifiedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
