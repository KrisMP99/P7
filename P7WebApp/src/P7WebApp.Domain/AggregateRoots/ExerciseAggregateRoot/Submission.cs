using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules;
using P7WebApp.SharedKernel;


namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot
{
    public class Submission : EntityBase
    {
        public Submission(string title, DateTime submitDate) 
        { 
            Title = title;
            SubmitDate = submitDate;
        }

        public string Title { get; set; }
        public DateTime SubmitDate { get; set; }
        public List<Module> Modules { get; set; }


        public void DeleteSubmission()
        {
            throw new NotImplementedException();
        }
    }
}
