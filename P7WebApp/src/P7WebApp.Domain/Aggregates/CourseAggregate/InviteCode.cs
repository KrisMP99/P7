using P7WebApp.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class InviteCode : EntityBase
    {
        public InviteCode(int courseId, bool isActive, DateTime? useableFrom, DateTime? useableTo)
        {
            CourseId = courseId;
            IsActive = isActive;
            UseableFrom = useableFrom ?? DateTime.UtcNow;
            UseableTo = useableTo ?? DateTime.MaxValue;
        }
        public int CourseId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime? UseableFrom { get; set; }
        public DateTime? UseableTo { get; set; }

        public void UpdateInformation(bool isActive, DateTime useableFrom, DateTime useableTo)
        {
            IsActive = isActive;
            UseableFrom = useableFrom;
            UseableTo = useableTo;
        }
    }
}
