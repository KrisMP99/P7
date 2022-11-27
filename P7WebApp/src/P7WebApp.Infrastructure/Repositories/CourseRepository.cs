using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Repositories;
using System.Reflection.Metadata;


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
        public async Task<int> GetCourseFromInviteCode(int code)
        {
            try
            {
                var course = await _context.Courses.Where(c => c.InviteCode.Code == code).FirstOrDefaultAsync();

                if (course != null)
                {
                    return course.Id;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
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

        public async Task<Course> GetCourseWithExerciseGroups(int courseId)
        {
            try
            {
                var course = await _context.Courses.Where(c => c.Id == courseId)
                                                   .Include(c => c.ExerciseGroups)
                                                   .Include(c => c.InviteCode)
                                                   .Include(c => c.Attendees)
                                                   .FirstOrDefaultAsync();
                //string sql = $"SELECT * FROM Courses JOIN ExerciseGroups ON Courses.Id = ExerciseGroups.CourseId JOIN InviteCode ON Course.Id = InviteCode.CourseId JOIN Attendees ON Course.Id = Attendees.CourseId JOIN AspNetUsers ON Attendees.UserId = AspNetUsers.Id";
                //var course = _context.Courses.FromSql($"SELECT * FROM public.\"Courses\" as c JOIN public.\"ExerciseGroups\" as eg ON c.\"Id\" = eg.\"CourseId\" JOIN public.\"InviteCode\" as ic ON c.\"Id\" = ic.\"CourseId\" JOIN public.\"Attendees\" as a ON c.\"Id\" = a.\"CourseId\" JOIN public.\"AspNetUsers\" as u ON a.\"UserId\" = u.\"Id\"").ToList();

                if (course != null)
                {
                    return course;
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

        // TODO: Implement correctly
        public async Task<IEnumerable<Course>> GetAttendedCourses(string userId)
        {
            try
            {
                var courses = _context.Courses.Include(c => c.Attendees).Where(c => c.Attendees.Any(a => a.Profile.UserId == userId));

                return courses;
            }
            catch (Exception)
            { 
                throw;
            }
            
        }


        public async Task<IEnumerable<ExerciseGroup>> GetExerciseGroupsWithExercises(int courseId)
        {
            try
            {
                var exerciseGroups = _context.ExerciseGroups.Where(e => e.CourseId == courseId).Include(e => e.Exercises);

                if (exerciseGroups != null)
                {
                    return exerciseGroups.AsEnumerable();

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

        public async Task<IEnumerable<Course>> GetListOfCourses()
        {

            var courses = await _context.Courses.ToListAsync();

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

        public async Task<IEnumerable<Course>> GetCreatedCourses(string userId)
        {
            try
            {
                var courses = _context.Courses.Where(c => c.Owner.UserId == userId);

                return courses.AsEnumerable();
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<IEnumerable<Course>> GetPublicCourses()
        {
            try
            {
                var courses = _context.Courses.Where(c => c.IsPrivate == false);
                return courses;
            }
            catch (Exception)
            {

                throw;
            }
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

        public async Task<Course> GetCourseWithAttendeesAndDefaultCourseRoles(int courseId)
        {
            try
            {
                var course = await _context.Courses
                    .Where(c => c.Id == courseId)
                    .Include(c => c.Attendees)
                    .Include(c => c.CourseRoles.Where(role => role.IsDefaultRole).FirstOrDefault())
                        .ThenInclude(role => role.Permission)
                    .FirstOrDefaultAsync();

                if (course is null)
                {
                    throw new Exception("Could not find course with default role and permission.");
                }

                return course;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
