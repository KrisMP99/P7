using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class DeleteExerciseGroupCommand : IRequest<int>
    {
        public DeleteExerciseGroupCommand(int exerciseId)
        {
            ExerciseGroupId = exerciseId;
        }

        public int ExerciseGroupId { get; set; }
    }
}