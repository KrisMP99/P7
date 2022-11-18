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
        public CreateExerciseCommand(int exerciseGroupId, string title, bool isVisible, int exerciseNumber, DateTime? startDate, DateTime? endDate, DateTime? visibleFrom, DateTime? visibleTo, int layoutId)
        {
            ExerciseGroupId = exerciseGroupId;
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
            VisibleFrom = visibleFrom;
            VisibleTo = visibleTo;
            LayoutId = layoutId;
        }
        public int ExerciseGroupId { get; }
        public string Title { get; }
        public bool IsVisible { get; }
        public int ExerciseNumber { get; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public DateTime? VisibleFrom { get; }
        public DateTime? VisibleTo { get;}
        public int LayoutId { get; }
    }
        //public int Layout { get; set; }
        //public IEnumerable<Module>? Modules { get; set; }
        //public IEnumerable<Solution>? Solutions { get; set; 
}
