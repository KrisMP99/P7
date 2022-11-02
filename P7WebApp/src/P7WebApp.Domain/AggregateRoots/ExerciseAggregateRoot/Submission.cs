using P7WebApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void DeleteSubmission()
        {
            throw new NotImplementedException();
        }
    }
}
