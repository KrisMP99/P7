using MediatR;
namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateExerciseGroupCommand : IRequest<int>
    {
        public int CourseId { get; set; }
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public DateTime BecomeVisibleAt { get; set; }

    }
}
