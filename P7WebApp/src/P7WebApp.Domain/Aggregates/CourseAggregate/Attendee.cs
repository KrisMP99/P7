using P7WebApp.Domain.Common;
namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Attendee : EntityBase
    {
        public Attendee(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
