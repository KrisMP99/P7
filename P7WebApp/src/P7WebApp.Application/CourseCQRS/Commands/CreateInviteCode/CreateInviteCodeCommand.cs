using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands.CreateInviteCode
{
    public class CreateInviteCodeCommand : IRequest<int>
    {
        public CreateInviteCodeCommand(int courseId, bool isActive)
        {
            CourseId = courseId;
            IsActive = isActive;
            UseableFrom = DateTime.UtcNow;
            UseableTo = DateTime.MaxValue;
        }
        public CreateInviteCodeCommand(int courseId, bool isActive, DateTime? useableFrom, DateTime? useableTo)
        {
            CourseId = courseId;
            IsActive = isActive;
            UseableFrom = useableFrom ?? DateTime.UtcNow;
            UseableTo = useableTo ?? DateTime.MaxValue;
        }

        public int CourseId { get; set; }

        public bool IsActive { get; set; }
        public DateTime? UseableFrom { get; }
        public DateTime? UseableTo { get; }

    }
}
