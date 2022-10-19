using MediatR;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class UpdateExerciseCommand : IRequest<int>
    {
        public UpdateExerciseCommand(string title, bool isVisible, int exerciseNumber, DateTime startDate, DateTime endDate)
        {
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
        }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}