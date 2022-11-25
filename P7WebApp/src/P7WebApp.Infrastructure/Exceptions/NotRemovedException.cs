using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Infrastructure.Exceptions
{
    public class NotRemovedException : Exception
    {
        public NotRemovedException() { }
        public NotRemovedException(string message) : base(message) { }
    }
}
