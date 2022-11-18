using MediatR;
using System.Runtime.InteropServices;

namespace P7WebApp.Application.ExerciseCQRS.Commands
{
    public class UpdateExerciseCommand : IRequest<int>
    {
        public UpdateExerciseCommand(int exerciseGroupId, string title, bool isVisible, int exerciseNumber, DateTime? startDate, DateTime? endDate, DateTime? visibleFrom, DateTime? visibleTo, int layoutId)
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
            LayoutId = layoutId;
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? VisibleFrom { get; set; }
        public DateTime? VisibleTo { get; set; }
        public DateTime CreatedDate { get; }
        public DateTime LastModifiedDate { get; }
        public int ExerciseGroupId { get; set; }
        public int LayoutId { get; set; }
    }
}