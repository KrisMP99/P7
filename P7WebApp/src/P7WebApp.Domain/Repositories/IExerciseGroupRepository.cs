using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface IExerciseGroupRepository
    {
        Task CreateExerciseGroup(ExerciseGroup course);
        Task<ExerciseGroup> GetExerciseGroupByGroupId(int exerciseGroupId);
        Task<IEnumerable<ExerciseGroup>> GetExerciseGroupsByCourseId(int courseId);
    }
}
