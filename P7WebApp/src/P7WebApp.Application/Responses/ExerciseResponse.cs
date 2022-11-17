using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;

namespace P7WebApp.Application.Responses
{
    public class ExerciseResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? VisableFrom { get; set; }
        public DateTime? VisibleTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public ExerciseLayout Layout { get; set; }
        public List<Module>? Modules { get; set; }
        public List<Solution>? Solution { get; set; }
        public List<Submission>? Submissions { get; set; }
    }
}
