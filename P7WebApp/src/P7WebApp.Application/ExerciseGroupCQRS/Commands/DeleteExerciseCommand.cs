using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands
{
    public class DeleteExerciseCommand : IRequest<int>
    {
        public DeleteExerciseCommand(int id)
        {
            this.id = id;
        }

        public int id { get; set; }
    }
}
