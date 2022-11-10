using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IApplicationDbContext _context;

        public CourseRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCourse(Course course)
        {
            return 1;
        }

        public async Task<int> DeleteCourse(int courseId)
        {
            return courseId;
        }

        public async Task<Course> GetCourse(int id)
        {
            return new Course() { Id = id};
        }

        public async Task<Course> GetCourseFromExerciseGroupId(int exerciseGroupId)
        {
            return new Course() { Id = 1};
        }

        public async Task<IEnumerable<ExerciseGroup>> GetExerciseGroups(int id)
        {
            return new List<ExerciseGroup>().AsEnumerable(); 
        }

        public async Task<IEnumerable<Course>> GetListOfCourses()
        {
            IEnumerable<Course> courses = new List<Course> { new Course { Id = 1 }, new Course { Id = 2 } };
            return courses;
        }

        public async Task<int> UpdateCourse(Course course)
        {
            return 1;
        }
    }
}
