using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.UpdateExerciseGroup
{
    public class UpdateExerciseGroupCommand : IRequest<int>
    {
        public UpdateExerciseGroupCommand(int id, int courseId, string title, string description, bool isVisible, int exerciseGroupNumber, DateTime? becomesVisibleAt)
        {
            Id = id;
            CourseId = courseId;
            Title = title;
            Description = description;
            IsVisible = isVisible;
            ExerciseGroupNumber = exerciseGroupNumber;
            BecomesVisibleAt = becomesVisibleAt;
        }
        public int CourseId { get; }
        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public bool IsVisible { get; }
        public int ExerciseGroupNumber { get; }
        public DateTime? BecomesVisibleAt { get; }
    }
}