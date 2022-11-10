using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.SharedKernel;


namespace P7WebApp.Domain.Aggregates.ExerciseAggregate
{
    public class Submission : EntityBase
    {
        public Submission(string title, DateTime submitDate)
        {
            Title = title;
            SubmitDate = submitDate;
        }

        public int SubmissionDraftId { get; private set; }
        public int ExerciseId { get; private set; }
        public string Title { get; private set; }
        public DateTime SubmitDate { get; private set; }
        public List<Module> Modules { get; private set; }


        public void DeleteSubmission()
        {
            throw new NotImplementedException();
        }
    }
}
