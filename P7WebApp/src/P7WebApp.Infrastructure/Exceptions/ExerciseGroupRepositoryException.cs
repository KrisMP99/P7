﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Infrastructure.Exceptions
{
    public class ExerciseGroupRepositoryException : Exception
    {
        public ExerciseGroupRepositoryException() { }
        public ExerciseGroupRepositoryException(string message) : base(message) { }
    }
}
