using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules;
using P7WebApp.Domain.AggregateRoots.ExerciseGroupAggregateRoot;
using P7WebApp.Domain.Common;
using P7WebApp.SharedKernel;
using P7WebApp.SharedKernel.Interfaces;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot
{
    public class Exercise : EntityBase, IAggregateRoot
    {
        public Exercise() { }
        public Exercise(string title, bool isVisible, int exerciseNumber, DateTime startDate, DateTime endDate, DateTime createdDate, DateTime lastModifiedDate, ExerciseLayout layout)
        {
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
            Layout = layout;
        }

        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public ExerciseLayout Layout { get; set; }
        public ExerciseType Type { get; set; }
        public List<Module> Modules { get; set; }

        public void UpdateExerciseInformation(string newTitle, bool visibility, int exerciseNumber, DateTime newStartDate, DateTime newEndDate)
        {
            throw new NotImplementedException();
        }

        public void AddModule(Module module)
        {
            try
            {
                if (module is not null)
                {
                    Modules.Add(module);
                } else
                {
                    throw new Exception("Could not add modules");
                }
            }
            catch (Exception)
            {

                throw;
            }
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