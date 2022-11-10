using MediatR;

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
        public int ExerciseGroupId { get; set; }
        public int Id { get; set; }
    }
}