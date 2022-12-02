using Microsoft.EntityFrameworkCore;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Exceptions;
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
                throw new CourseRepositoryException($"Could not create course: {course.Title}.");
            }
        }
        public async Task<int> GetCourseIdFromInviteCode(int code)
        {
            try
            {
                int courseId = await _context.Courses.Where(c => c.InviteCode.Code == code).Select(c => c.Id).FirstOrDefaultAsync();

                if (courseId > 0)
                {
                    return courseId;
                }
                else
                {
                    throw new CourseRepositoryException($"Invite code: {code} does not exsist.");
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
                    throw new CourseRepositoryException($"Could not remove course with id {courseId} (does not exsist).");
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
                var course = await _context.Courses
                    .Where(c => c.Id == courseId)
                    .Include(c => c.ExerciseGroups)
                    .FirstOrDefaultAsync();

                if (course is null)
                {
                    throw new CourseRepositoryException($"Could not get course with Id: {courseId}.");
                }

                return course;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<Course> GetCourseWithExerciseGroupsAndExercisesAndAttendess(int courseId)
        {
            try
            {
                var course = await _context.Courses
                    .Where(c => c.Id == courseId)
                    .Include(c => c.Attendees)
                        .ThenInclude(a => a.Profile)
                    .Include(c => c.ExerciseGroups)
                    .ThenInclude(eg => eg.Exercises)
                    .FirstOrDefaultAsync();

                if (course is null)
                {
                    throw new Exception("Could not get course with exercise groups.");
                }

                return course;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Course> GetCourseWithExerciseGroupsAttendeesAndInviteCode(int courseId)
        {
            try
            {
                var course = await _context.Courses
                    .Where(c => c.Id == courseId)
                    .Include(c => c.ExerciseGroups)
                    .Include(c => c.InviteCode)
                    .Include(c => c.Attendees)
                        .ThenInclude(a => a.Profile)
                    .FirstOrDefaultAsync();
               
                if (course is null)
                {
                    throw new Exception();
                }

                return course;
            }
            catch (Exception)
            {
                throw new CourseRepositoryException($"Could not find course with id: {courseId} (does not exsist).");
            }
        }

        public async Task<IEnumerable<Course>> GetAttendedCourses(string userId)
        {
            try
            {
                var courses = _context.Courses
                    .Include(c => c.Attendees)
                    .Where(c => c.Attendees.Any(a => a.Profile.UserId == userId));

                return courses;
            }
            catch (Exception)
            { 
                throw new CourseRepositoryException($"Could not find attended courses for user Id: {userId}.");
            }
            
        }


        public async Task<IEnumerable<ExerciseGroup>> GetExerciseGroupsWithExercises(int courseId)
        {
            try
            {
                var exerciseGroups = _context.ExerciseGroups
                    .Where(e => e.CourseId == courseId)
                    .Include(e => e.Exercises);

                if (exerciseGroups is null)
                {
                    throw new CourseRepositoryException($"Could not get exercisegroups with exercises for course Id: {courseId}.");
                }

                return exerciseGroups.AsEnumerable();
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
                if (courses is null)
                {
                    throw new CourseRepositoryException("Could not get list of courses.");
                }

                return courses;
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
                throw new CourseRepositoryException($"Could not get created courses for user: {userId}.");
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
                    throw new CourseRepositoryException("Could not update the course");
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
                    .Include(c => c.CourseRoles.Where(role => role.IsDefaultRole == true).Take(1))
                        .ThenInclude(role => role.Permission)
                    .FirstOrDefaultAsync();

                if (course is null)
                {
                    throw new CourseRepositoryException("Could not find course with default role and permission.");
                }

                return course;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Course> GetCourseWithAttendees(int courseId)
        {
            try
            {
                var course = await _context.Courses
                    .Where(c => c.Id == courseId)
                    .Include(c => c.Attendees)
                    .FirstOrDefaultAsync();

                return course;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
