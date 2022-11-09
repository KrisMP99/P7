using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class UpdateExerciseGroupCommand : IRequest<int>
    {
        public UpdateExerciseGroupCommand(string title, string description, bool isVisible, int exerciseGroupNumber, DateTime becomesVisibleAt)
        {
            Title = title;
            Description = description;
            IsVisible = isVisible;
            ExerciseGroupNumber = exerciseGroupNumber;
            BecomesVisibleAt = becomesVisibleAt;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseGroupNumber { get; set; }
        public DateTime BecomesVisibleAt { get; set; }
        public int ExerciseGroupId { get; set; }
    }
}