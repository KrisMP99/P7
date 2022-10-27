using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateInviteCommand : IRequest<int>
    {
        public CreateInviteCommand(int courseId, bool isActive, DateTime useableFrom, DateTime useableTo)
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
