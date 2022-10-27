using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<int> CreateCourse(Course course);
        Task<Course> GetCourse(int id);
        Task<Course> GetCourseFromExerciseGroupId(int exerciseGroupId);
        Task<Exercise> GetExercise(int id); // Should this be moved to the exercisegroup?
        Task<IEnumerable<ExerciseGroup>> GetExerciseGroups(int id);
        Task<int> UpdateCourse(Course course);
  
    }
}
