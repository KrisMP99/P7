using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class UpdateExerciseCommand : IRequest<int>
    {
        public UpdateExerciseCommand(string title, bool isVisible, int exerciseNumber, DateTime startDate, DateTime endDate, int courseId)
        {
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
            CourseId = courseId;
        }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ExerciseGroupId { get; set; }
        public int Id { get; set; }
    }
}