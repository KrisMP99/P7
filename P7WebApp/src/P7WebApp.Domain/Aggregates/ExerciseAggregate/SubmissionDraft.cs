using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.SharedKernel;


namespace P7WebApp.Domain.Aggregates.ExerciseAggregate
{
    public class SubmissionDraft : EntityBase
    {
        public SubmissionDraft(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
        public List<Module> Modules { get; set; }
        public List<Submission> Submissions { get; set; }

        public void Submit()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
