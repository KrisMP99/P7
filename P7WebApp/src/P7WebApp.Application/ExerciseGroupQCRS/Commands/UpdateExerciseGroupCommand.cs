using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace P7WebApp.Application.ExerciseGroupQCRS.Commands
{
    public class UpdateExerciseGroupCommand : IRequest<int>
    {
        public UpdateExerciseGroupCommand(int id, int courseId, string title, string description, bool isVisible, int exerciseGroupNumber, DateTime becomesVisibleAt)
        {
            Id = id;
            CourseId = courseId;
            Title = title;
            Description = description;
            IsVisible = isVisible;
            ExerciseGroupNumber = exerciseGroupNumber;
            BecomesVisibleAt = becomesVisibleAt;
        }
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseGroupNumber { get; set; }
        public DateTime BecomesVisibleAt { get; set; }
    }
}