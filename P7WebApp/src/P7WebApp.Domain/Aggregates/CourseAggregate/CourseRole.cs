using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class CourseRole : EntityBase
    {
        public CourseRole()
        {

        }

        public string RoleName { get; private set; }

        public Permission Permisson { get; private set; }
        public int CourseId { get; private set; }

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
