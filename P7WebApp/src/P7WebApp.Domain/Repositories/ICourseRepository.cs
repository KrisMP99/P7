using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseGroupAggregateRoot;

namespace P7WebApp.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<int> CreateCourse(Course course);
        Task<int> DeleteCourse(int courseId);
        Task<Course> GetCourse(int id);
        Task<Course> GetCourseFromExerciseGroupId(int exerciseGroupId);
        Task<IEnumerable<ExerciseGroup>> GetExerciseGroups(int id);
        Task<int> UpdateCourse(Course course);
  
    }
}
