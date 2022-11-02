using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateInviteCodeCommand : IRequest<int>
    {
        public CreateInviteCodeCommand(int courseId, bool isActive, DateTime useableFrom, DateTime useableTo)
        {
            CourseId = courseId;
            IsActive = isActive;
            UseableFrom = useableFrom;
            UseableTo = useableTo;
        }
        
        public int CourseId { get; set; }

        public bool IsActive { get; set; }
        public DateTime UseableFrom { get; set; }
        public DateTime UseableTo { get; set; }

    }
} 
