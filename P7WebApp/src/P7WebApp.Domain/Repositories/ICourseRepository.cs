using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<int> CreateCourse(Course course);
        Task<int> DeleteCourse(int courseId);
        Task<Course> GetCourse(int id);
        Task<IEnumerable<ExerciseGroup>> GetExerciseGroups(int courseId);
        Task<IEnumerable<Course>> GetListOfCourses();
        Task<int> UpdateCourse(Course course);
  
    }
}
