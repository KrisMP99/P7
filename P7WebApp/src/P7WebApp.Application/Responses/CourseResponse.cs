using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using System.Reflection.Metadata.Ecma335;

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
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }  
        public IEnumerable<ExerciseGroupResponse> ExerciseGroups { get; set; }
        public IEnumerable<AttendeeResponse> Attendees { get; set; }
    }
}
