using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.Responses
{
    public class CourseResponse
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int OwnerId { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
