using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface IExerciseGroupRepository
    {
        Task<IAsyncEnumerable<ExerciseGroup>> GetExerciseGroupsByCourseId(int courseId);
        Task<ExerciseGroup> GetExerciseGroupByIdWithExercises(int Id);
        Task<int> UpdateExerciseGroup(ExerciseGroup course);
        Task CreateExercise(Exercise exercise);
        Task<int> DeleteExercise(int exerciseId);
    }
}
