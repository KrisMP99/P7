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
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? VisibleFrome { get; set; }
        public DateTime? VisibleTo { get; set; }
        public int Layout { get; set; }
        public IEnumerable<Module>? Modules { get; set; }
        public IEnumerable<Solution> Solutions { get; set; }
    }
}
