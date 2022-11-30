using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class LeaveCourseCommand : IRequest<int>
    {
        public LeaveCourseCommand(int courseId)
        {
            CourseId = courseId;
        }

        public int CourseId { get; }
    }
}
