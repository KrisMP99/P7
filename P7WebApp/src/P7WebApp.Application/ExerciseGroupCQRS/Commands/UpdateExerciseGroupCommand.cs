using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands
{
    public class UpdateExerciseGroupCommand : IRequest<int>
    {
        public UpdateExerciseGroupCommand(string title, string description, bool isVisible, int exerciseGroupNumber, DateTime becomesVisibleAt, int courseId)
        {
            Id = id;
            CourseId = courseId;
            Title = title;
            Description = description;
            IsVisible = isVisible;
            ExerciseGroupNumber = exerciseGroupNumber;
            BecomesVisibleAt = becomesVisibleAt;
            CourseId = courseId;
        }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseGroupNumber { get; set; }
        public DateTime BecomesVisibleAt { get; set; }
    }
}