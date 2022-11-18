using Microsoft.EntityFrameworkCore;
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

        public async Task CreateCourse(Course course)
        {
            try 
            {
                await _context.Courses.AddAsync(course);
            }
            catch(Exception)
            {
                throw;
            }
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
        public async Task<Course> GetCourse(int courseId)
        {
            try
            {
                var course = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();

                if (course is not null)
                {
                    return course;
                }
                else
                {
                    throw new Exception($"Course with {courseId} could not be found");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<Course>> GetAttendedCourses(int userId)
        {
            try
            {
                var courses = _context.Courses.Where(c => c.Id == userId);

                if(courses.Any())
                {
                    return courses;
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

        public async Task<IEnumerable<Course>> GetListOfCourses()
        {

            var courses = _context.Courses.ToList().AsEnumerable();

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

        public async Task<IEnumerable<Course>> GetUsersCreatedCourses(string userId)
        {
            try
            {
                var courses = _context.Courses
                    .Where(c => c.CreatedById == userId);
                
                if(courses is not null)
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

        public Task<IEnumerable<Course>> GetPublicCourses()
        {
            throw new NotImplementedException();
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

        public Task<Course> GetCourseFromExerciseGroupId(int exerciseGroupId)
        {
            throw new NotImplementedException();
        }
    }
}
