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
            try
            {
                var course = _context.Courses.Find(courseId);

                if (course != null)
                {
                    _context.Courses.Remove(course);
                    return courseId;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Course> GetCourse(int id)
        {
            try
            {
                var course = _context.Courses.Find(id);

                if(course != null)
                {
                    return course;

                } else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }


        public async Task<IEnumerable<ExerciseGroup>> GetExerciseGroups(int courseId)
        {
            try
            {
                var exerciseGroups = _context.ExerciseGroups.Where(e => e.CourseId == courseId);

                if (exerciseGroups != null)
                {
                    return exerciseGroups.AsEnumerable();

                } else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<Course>> GetListOfCourses(int courseId)
        {

            var courses = _context.Courses.Where(e => e.CourseId == courseId);

            try
            {
                if (courses != null)
                {
                    return courses;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<int> UpdateCourse(Course course)
        {
            try
            {
                var courseUpdate = _context.Courses.Update(course);

                if (courseUpdate != null)
                {
                    return 1;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
