using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules.QuizModule
{
    public class Choice
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public void UpdateInformation()
        {
            throw new NotImplementedException();
        }
    }
}
