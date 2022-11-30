using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Infrastructure.Exceptions
{
    public class CourseRepositoryException : Exception
    {
        public CourseRepositoryException() { }
        public CourseRepositoryException(string message) : base(message) { }

    }
}
