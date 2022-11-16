using P7WebApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate
{
    public class ExerciseOverview : EntityBase
    {
        public string Title { get; private set; }
        public bool IsVisible { get; private set; }
        public int ExerciseNumber { get; private set; }

    }
}
