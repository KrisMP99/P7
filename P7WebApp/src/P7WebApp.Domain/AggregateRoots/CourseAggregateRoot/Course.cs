using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class Course : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int OwnerId { get; set; }
        public List<Exercise> Exercises { get; set; }

    }
}
