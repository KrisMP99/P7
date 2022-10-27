using P7WebApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class InviteCode : EntityBase
    {
        public InviteCode(int courseId, bool isActive, DateTime useableFrom, DateTime useableTo)
        {
            CourseId = courseId;
            IsActive = isActive;
            UseableFrom = useableFrom;
            UseableTo = useableTo;
        }
        public int CourseId { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime UseableFrom { get; set; }
        public DateTime UseableTo { get; set; }

        public void UpdateInformation(bool isActive, DateTime useableFrom, DateTime useableTo)
        {
            IsActive = isActive;
            UseableFrom = useableFrom;
            UseableTo = useableTo;
        }
    }
}
