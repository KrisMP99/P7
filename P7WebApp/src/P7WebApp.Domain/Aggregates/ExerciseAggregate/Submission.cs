using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Common;


namespace P7WebApp.Domain.Aggregates.ExerciseAggregate
{
    public class Submission : EntityBase
    {
        public Submission(int userId, int exerciseId, bool isSubmitted, string title)
        {
            UserId = userId;
            ExerciseId = exerciseId;
            IsSubmitted = isSubmitted;
            Title = title;
            SubmitDate = DateTime.UtcNow;
        }

        public int UserId { get; set; }
        public int ExerciseId { get; private set; }
        public string Title { get; private set; }
        public bool IsSubmitted { get; set; }
        public DateTime SubmitDate { get; private set; }
        public List<Module> Modules { get; private set; }
    }
}