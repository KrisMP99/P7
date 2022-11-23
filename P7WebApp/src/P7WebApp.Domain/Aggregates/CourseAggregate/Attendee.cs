using P7WebApp.Domain.Common;
namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Attendee : EntityBase
    {
        // An attendee just have an application user
        // I.e., the user who attends the cours
        public string UserId { get; set; }
    }
}
