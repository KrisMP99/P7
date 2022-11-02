using P7WebApp.Domain.Common;
using P7WebApp.SharedKernel;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class Exercise : EntityBase 
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

        public void UpdateInformation(string newTitle, bool visibility, int exerciseNumber, DateTime newStartDate, DateTime newEndDate)
        {
            throw new NotImplementedException();
        }

        public Module AddModule(string title, DateTime createdDate, ExerciseLayout layout)
        {
            throw new NotImplementedException();
        }

        public Module DeleteModule(string title)
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