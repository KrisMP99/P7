using P7WebApp.Domain.Aggregates.ProfileAggregate;
using P7WebApp.Domain.Common;
namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Attendee : EntityBase
    {
        public Attendee(int courseId, int courseRoleId, int profileId)
        {
            CourseId = courseId;
            CourseRoleId = courseRoleId;
            ProfileId = profileId;
        }

        public int CourseId { get; private set; }
        public int CourseRoleId { get; private set; }
        public CourseRole CourseRole { get; private set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
