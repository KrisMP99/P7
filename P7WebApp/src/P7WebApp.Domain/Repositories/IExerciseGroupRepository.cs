using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface IExerciseGroupRepository
    {
        Task<ExerciseGroup> GetExerciseGroupByGroupId(int exerciseGroupId);
        Task<IAsyncEnumerable<ExerciseGroup>> GetExerciseGroupsByCourseId(int courseId);
    }
}
