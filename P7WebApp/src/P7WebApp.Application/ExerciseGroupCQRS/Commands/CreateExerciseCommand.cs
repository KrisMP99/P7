using MediatR;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands
{
    public class CreateExerciseCommand : IRequest<int>
    {
        public CreateExerciseCommand(int exerciseGroupId, string title, bool isVisible, int exerciseNumber, DateTime? startDate, DateTime? endDate, DateTime? visibleFrom, DateTime? visibleTo)
        {
            ExerciseGroupId = exerciseGroupId;
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate ?? DateTime.UtcNow;
            EndDate = endDate ?? DateTime.MaxValue;
            VisibleFrom = visibleFrom ?? DateTime.UtcNow;
            VisibleTo = visibleTo ?? DateTime.MaxValue;
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;
        }
        public int ExerciseGroupId { get; private set; }
        public string Title { get; private set; }
        public bool IsVisible { get; private set; }
        public int ExerciseNumber { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? VisibleFrom { get; private set; }
        public DateTime? VisibleTo { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
    }
        //public int Layout { get; set; }
        //public IEnumerable<Module>? Modules { get; set; }
        //public IEnumerable<Solution>? Solutions { get; set; 
}
