using MediatR;

namespace P7WebApp.Application.ExerciseCQRS.Commands
{
    public class UpdateExerciseCommand : IRequest<int>
    {
        public UpdateExerciseCommand(int id, string title, bool isVisible, int exerciseNumber, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? VisibleFrom { get; set; }
        public DateTime? VisibleTo { get; set; }
        public int ExerciseGroupId { get; set; }
    }
}