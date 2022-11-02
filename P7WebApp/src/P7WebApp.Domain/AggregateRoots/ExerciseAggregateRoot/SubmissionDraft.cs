using P7WebApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot
{
    public class SubmissionDraft : EntityBase
    {
        public SubmissionDraft(string title) 
        { 
            Title = title;
        }
        
        public string Title { get; set; }

        public void Submit()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
