using MediatR;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.ExerciseCQRS.Queries.Get
{
    public class GetExerciseQuery : IRequest<ExerciseResponse>
    {
        public GetExerciseQuery(int courseId, int exerciseGroupId, int exerciseId)
        {
            CourseId = courseId;
            ExerciseGroupId = exerciseGroupId;
            ExerciseId = exerciseId;
        }

        public int CourseId { get; }
        public int ExerciseGroupId { get; }
        public int ExerciseId { get; }
    }
}
