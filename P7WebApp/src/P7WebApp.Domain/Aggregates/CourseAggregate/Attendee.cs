using P7WebApp.Domain.Common;
using P7WebApp.Domain.Identity;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Attendee : EntityBase
    {
        // An attendee just have an application user
        // I.e., the user who attends the cours
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
