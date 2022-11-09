using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateExerciseModuleCommand : IRequest<int>
    {
        public CreateExerciseModuleCommand(int exerciseId, string title, bool isVisible, int exerciseNumber, DateTime startDate, DateTime endDate, DateTime createdDate)
        {
            ExerciseId = exerciseId;
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
            CreatedDate = createdDate;
        }

        public int ExerciseId { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
