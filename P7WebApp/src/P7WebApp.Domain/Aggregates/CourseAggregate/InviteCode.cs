using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
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
