using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot
{
    public class ExerciseStatistics
    {
        public int CourseId { get; set; }
        public int Attendees { get; set; }
        public List<ExerciseType>? CommonMistakes { get; set; }
    }
}
