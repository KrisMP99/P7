using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class CourseRole : EntityBase
    {
        public CourseRole()
        {

        }

        public string RoleName { get; private set; }

        public List<Permission> Permissons;

        public void AddPermission()
        {
            throw new NotImplementedException();
        }

        public void DeletePermission()
        {
            throw new NotImplementedException();
        }

        public void EditInformation(string name)
        {
            RoleName = name;
        }

    }
}
