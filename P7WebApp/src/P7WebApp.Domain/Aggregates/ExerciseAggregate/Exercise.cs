using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.SharedKernel;
using P7WebApp.SharedKernel.Interfaces;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate
{
    public class Exercise : EntityBase, IAggregateRoot
    {
        public Exercise() { }
        public Exercise(string title, bool isVisible, int exerciseNumber, DateTime startDate, DateTime endDate, DateTime createdDate, DateTime lastModifiedDate)
        {
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
        }

        public int ExerciseGroupId { get; private set; }
        public string Title { get; private set; }
        public bool IsVisible { get; private set; }
        public int ExerciseNumber { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public List<Module> Modules { get; private set; }
        public List<Submission> Submissions { get; private set; }

        public void UpdateInformation(string newTitle, bool visibility, int exerciseNumber, DateTime newStartDate, DateTime newEndDate)
        {
            throw new NotImplementedException();
        }

        public void AddModule(string title, DateTime createdDate, ExerciseLayout layout)
        {
            throw new NotImplementedException();
        }

        public void DeleteModule(string title)
        {
            throw new NotImplementedException();
        }

        public void AddSolution(string title, int exerciseNumber, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void DeleteSolution(string title, int exerciseNumber)
        {
            throw new NotImplementedException();
        }

        public void AddSubmission(string title, int exerciseNumber)
        {
            throw new NotImplementedException();
        }

        public void DeleteSubmission(string title, int exerciseNumber)
        {
            throw new NotImplementedException();
        }
    }
}