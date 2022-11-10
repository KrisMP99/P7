﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateSolutionCommand : IRequest<int>
    {

        public int SolutionId { get; set; }
        public bool IsVisible { get; set; }
        public DateTime VisibleFromDate { get; set; }
    }
}
