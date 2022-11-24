using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task CreateCourse(Course course);
        Task<int> DeleteCourse(int courseId);
        Task<int> GetCourseFromInviteCode(int code);
        Task<Course> GetCourseWithAttendees(int courseId);
        Task<Course> GetCourseWithExerciseGroups(int courseId);
        Task<IEnumerable<Course>> GetListOfCourses();
        Task<IEnumerable<ExerciseGroup>> GetExerciseGroupsWithExercises(int courseId);
        Task<int> UpdateCourse(Course course);
        Task<IEnumerable<Course>> GetAttendedCourses(string userId);
        Task<IEnumerable<Course>> GetPublicCourses();
        Task<IEnumerable<Course>> GetCreatedCourses(string userId);
    }
}
