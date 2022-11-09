using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<int> CreateCourse(Course course);
        Task<Course> GetCourse(int id);
        Task<Course> GetCourseFromExerciseGroupId(int exerciseGroupId);
        Task<IEnumerable<ExerciseGroup>> GetExerciseGroups(int id);
        Task<int> UpdateCourse(Course course);
  
    }
}
