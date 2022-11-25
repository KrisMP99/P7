using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Infrastructure.Exceptions
{
    public class NotCreatedException : Exception
    {
        public NotCreatedException() { }
        public NotCreatedException(string message) : base(message) { }
    }
}
