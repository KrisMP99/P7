using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Infrastructure.Exceptions
{
    public class ExerciseRepositoryException : Exception
    {
        public ExerciseRepositoryException() { }
        public ExerciseRepositoryException(string message) : base(message) { }
    }
}
