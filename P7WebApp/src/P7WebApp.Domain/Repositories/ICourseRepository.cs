using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<int> AddCourse(Course course);
        Task<Course> GetCourse(int id);
        Task<Course> GetCourseFromExerciseGroupId(object exerciseGroupId);
        Task<Exercise> GetExercise(int id);
        Task<IEnumerable<ExerciseGroup>> GetExerciseGroups(int id);
        Task<int> UpdateCourse(Course course);
    }
}
