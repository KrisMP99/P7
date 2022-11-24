using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.DeleteExercise
{
    public class DeleteExerciseCommand : IRequest<int>
    {
        public DeleteExerciseCommand(int id, int exerciseGroupId)
        {
            Id = id;
            ExerciseGroupId = exerciseGroupId;
        }

        public int Id { get; set; }
        public int ExerciseGroupId { get; set; }
    }
}
