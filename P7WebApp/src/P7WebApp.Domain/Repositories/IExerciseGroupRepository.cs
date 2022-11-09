using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface IExerciseGroupRepository
    {
        Task<int> CreateExerciseGroup(ExerciseGroup course);
        Task<ExerciseGroup> GetExerciseGroupByGroupId(int exerciseGroupId);
        Task<ExerciseGroup> GetExerciseGroupByCourseId(int courseId);
        Task<IEnumerable<ExerciseGroup>> GetExerciseGroupsByCourseId(int courseId);
        Task<int> UpdateExerciseGroup(ExerciseGroup course);
    }
}
