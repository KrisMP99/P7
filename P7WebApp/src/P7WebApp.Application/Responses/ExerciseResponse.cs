using P7WebApp.Application.Responses.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;

namespace P7WebApp.Application.Responses
{
    public class ExerciseResponse
    {
        public int Id { get; set; }
        public int ExerciseGroupId { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? VisibleFrom { get; set; }
        public DateTime? VisibleTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public ICollection<ModuleResponse> Modules { get; set; } 
        public int LayoutId { get; set; }

    }
}
