using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands.DeleteExerciseGroup
{
    public class DeleteExerciseGroupCommand : IRequest<int>
    {
        public DeleteExerciseGroupCommand(int courseId, int exerciseId)
        {
            ExerciseGroupId = exerciseId;
            CourseId = courseId;
        }

        public int ExerciseGroupId { get; set; }
        public int CourseId { get; set; }
    }
}