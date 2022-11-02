using P7WebApp.Domain.Common;
using P7WebApp.SharedKernel;
using System.Reflection.Metadata.Ecma335;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot
{
    public class Exercise : EntityBase
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

        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public ExerciseType Type { get; set; }

        public void UpdateInformation(string newTitle, bool visibility, int exerciseNumber, DateTime newStartDate, DateTime newEndDate)
        {
            Title = string.IsNullOrEmpty(newTitle) ? Title : newTitle;
            IsVisible = visibility;
            ExerciseNumber = exerciseNumber;
            StartDate = newStartDate;
            EndDate = newEndDate;
        }
    }
}