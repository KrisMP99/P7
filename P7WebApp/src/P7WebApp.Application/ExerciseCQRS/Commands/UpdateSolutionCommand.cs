using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.Commands
{
    public class UpdateSolutionCommand : IRequest<int>
    {
        public UpdateSolutionCommand(int solutionId, int exerciseId, bool isVisible, DateTime? visibleFromDate)
        {
            SolutionId = solutionId;
            ExerciseId = exerciseId;
            IsVisible = isVisible;
            VisibleFromDate = visibleFromDate ?? DateTime.UtcNow;
        }
        public int SolutionId { get; set; }
        public int ExerciseId { get; private set; }
        public bool IsVisible { get; set; }
        public DateTime? VisibleFromDate { get; set; }
    }
}
