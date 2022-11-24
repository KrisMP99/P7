using P7WebApp.Domain.Common;
namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Attendee : EntityBase
    {
        public Attendee(string userId, int courseId)
        {
            UserId = userId;
            CourseId = courseId;
        }

        public string UserId { get; }
        public int CourseId { get; }
    }
}
