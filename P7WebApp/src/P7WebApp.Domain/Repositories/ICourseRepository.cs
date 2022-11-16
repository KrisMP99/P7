using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<int> CreateCourse(Course course);
        Task<int> DeleteCourse(int courseId);
        Task<Course> GetCourse(int courseId);
        Task<Course> GetCourseFromExerciseGroupId(int exerciseGroupId);
        Task<IEnumerable<ExerciseGroup>> GetExerciseGroups(int id);
        Task<IEnumerable<Course>> GetListOfCourses();
        Task<int> UpdateCourse(Course course);
        Task<IEnumerable<CourseOverview>> GetOwnedCourses(int userId);
        Task<IEnumerable<CourseOverview>> GetAttendedCourses(int userId);
        Task<IEnumerable<CourseOverview>> GetPublicCourses();

  
    }
}
