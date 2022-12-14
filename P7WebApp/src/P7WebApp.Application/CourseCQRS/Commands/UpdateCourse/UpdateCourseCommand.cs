using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<int>
    {
        public UpdateCourseCommand(int id, string title, string description, bool isPrivate)
        {
            Id = id;
            Title = title;
            Description = description;
            IsPrivate = isPrivate;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}