namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class CourseRole
    {
        public CourseRole()
        {

        }

        public string Name { get; private set; }

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
            Name = name;
        }

    }
}
