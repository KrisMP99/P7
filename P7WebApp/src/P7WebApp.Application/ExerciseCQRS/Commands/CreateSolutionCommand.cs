using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.Commands
{
    public class CreateSolutionCommand : IRequest<int>
    {
        public CreateSolutionCommand(bool isVisible, int exerciseId, DateTime visibleFromDate)
        {
            IsVisible = isVisible;
            ExerciseId = exerciseId;
            VisibleFromDate = visibleFromDate;
        }
        public bool IsVisible { get; set; }
        public int ExerciseId { get; set; }
        public DateTime VisibleFromDate { get; set; }
    }
}
