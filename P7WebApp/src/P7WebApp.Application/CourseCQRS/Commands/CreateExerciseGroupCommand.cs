using MediatR;
namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateExerciseGroupCommand : IRequest<int>
    {
        public CreateExerciseGroupCommand(int id, string title, string description, bool isVisible, DateTime becomeVisibleAt)
        {
            Id = id;
            Title = title;
            Description = description;
            IsVisible = isVisible;
            BecomeVisibleAt = becomeVisibleAt;
        }

        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public DateTime BecomeVisibleAt { get; set; }

    }
}
