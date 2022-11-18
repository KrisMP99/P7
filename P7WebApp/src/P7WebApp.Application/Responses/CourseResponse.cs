using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Application.Responses
{
    public class CourseResponse
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedById { get; set; }
        public string OwnerName { get; set; }  
        public IEnumerable<ExerciseGroupResponse> ExerciseGroups { get; set; }
    }
}
