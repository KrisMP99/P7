using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class CourseRole : EntityBase
    {
        public CourseRole(string roleName, bool isDefaultRole = false)
        {
            RoleName = roleName;
            Permission = new Permission(base.Id);
            IsDefaultRole = isDefaultRole;
        }

        public bool IsDefaultRole { get; private set; }
        public string RoleName { get; private set; }

        public Permission Permission { get; private set; }
        public int CourseId { get; private set; }

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
