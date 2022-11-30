using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.Responses
{
    public class ExerciseOverviewResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime VisibleFrom { get; set; }
        public DateTime ViaibleTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}