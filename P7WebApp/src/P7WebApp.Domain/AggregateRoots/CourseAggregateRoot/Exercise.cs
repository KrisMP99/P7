using P7WebApp.Domain.Common;
using P7WebApp.SharedKernel;
using System.Reflection.Metadata.Ecma335;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class Exercise : EntityBase
    {
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int CourseId { get; set; }
        public int ExerciseGroupId { get; set; }
        public ExerciseType Type { get; set; }

    }
}