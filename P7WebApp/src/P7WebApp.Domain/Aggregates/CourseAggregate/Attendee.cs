using P7WebApp.Domain.Common;
namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Attendee : EntityBase
    {
        public Attendee(string userId, int courseId, int roleId)
        {
            UserId = userId;
            CourseId = courseId;
            RoleId = roleId;
        }

        public string UserId { get; private set; }
        public int CourseId { get; private set; }
        public int RoleId { get; private set; }
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
