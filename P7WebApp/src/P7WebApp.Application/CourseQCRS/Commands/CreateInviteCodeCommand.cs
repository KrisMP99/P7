using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateInviteCodeCommand : IRequest<int>
    {
        public CreateInviteCodeCommand(int courseId, bool isActive, DateTime usableFrom, DateTime usableTo)
        {
            CourseId = courseId;
            IsActive = isActive;
            UsableFrom = usableFrom;
            UsableTo = usableTo;
        }
        
        public int CourseId { get; set; }

        public bool IsActive { get; set; }
        public DateTime? UsableFrom { get; set; }
        public DateTime? UsableTo { get; set; }

    }
} 
