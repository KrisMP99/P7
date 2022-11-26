using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class CourseRole : EntityBase
    {
        // Constructor for EF core
        private CourseRole() { }
        public CourseRole(int courseId, string roleName, Permission permission)
        {
            CourseId = courseId;
            RoleName = roleName;
            Permission = permission;
        }

        // Only used for creating a default course role
        // when a course is created
        private CourseRole(int courseId, Permission? permission = null)
        {
            CourseId = courseId;
            RoleName = "Attendee";
            IsDefaultRole = true;

            if(permission is null)
            {
                Permission = new Permission(base.Id);
            }
            else
            {
                Permission = permission;
            }
        }

        public bool IsDefaultRole { get; private set; } = false;
        public string RoleName { get; private set; }

        public Permission Permission { get; private set; }
        public int CourseId { get; private set; }

        public static CourseRole CreateDefaultCourseRole(int courseId, Permission? permission = null)
        {
            return new CourseRole(courseId, permission);
        }

        public void UpdatePermission(Permission permission)
        {
            Permission = permission;
        }

        public void EditInformation(string name)
        {
            RoleName = name;
        }
    }
}
