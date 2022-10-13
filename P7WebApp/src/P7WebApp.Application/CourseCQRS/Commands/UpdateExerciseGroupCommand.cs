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
        public UpdateExerciseCommand(string desription, bool isVisible, int exerciseGroupNumber, DateTime becomesVisibleAt)
        {
            Description = description;
            IsVisible = isVisible;
            ExerciseGroupNumber = exerciseGroupNumber;
            BecomesVisibleAt = becomesVisibleAt;
        }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseGroupNumber { get; set; }
        public DateTime BecomesVisibleAt { get; set; }
    }
}