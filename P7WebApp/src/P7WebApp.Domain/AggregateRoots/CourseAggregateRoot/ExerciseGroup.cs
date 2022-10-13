using P7WebApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class ExerciseGroup : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseGroupNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime BecomesVisibleAt { get; set; }
        public int CourseId { get; set; }
    }
}
