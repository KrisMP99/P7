using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Exceptions;


namespace P7WebApp.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(IApplicationDbContext context, ILogger<CourseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateCourse(Course course)
        {
            try 
            {
                await _context.Courses.AddAsync(course);
                _logger.LogInformation("create course in repository");
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Create course failed with message: {ex.Message}");
                throw new CourseRepositoryException($"Could not create course: {course.Title} due to {ex.Message}.");
            }
        }
        public async Task<int> GetCourseIdFromInviteCode(int code)
        {
            try
            {
                int courseId = await _context.Courses.Where(c => c.InviteCode.Code == code).AsNoTracking().Select(c => c.Id).FirstOrDefaultAsync();

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
                    .AsNoTracking()
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

        public async Task<IEnumerable<Course>> GetAttendedCourses(int profileId)
        {
            try
            {
                var courses = await  _context.Courses
                    .Include(c => c.Attendees)
                    .Where(c => c.Attendees.Any(a => a.Profile.Id == profileId))
                    .AsNoTracking()
                    .ToListAsync();

                return courses.AsEnumerable();
            }
            catch (Exception)
            { 
                throw new CourseRepositoryException($"Could not find attended courses for user Id: {profileId}.");
            }
            
        }


        public async Task<IEnumerable<ExerciseGroup>> GetExerciseGroupsWithExercises(int courseId)
        {
            try
            {
                var exerciseGroups = await _context.ExerciseGroups
                    .Where(e => e.CourseId == courseId)
                    .Include(e => e.Exercises)
                    .AsNoTracking()
                    .ToListAsync();

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

            var courses = await _context.Courses
                .AsNoTracking()
                .ToListAsync();

            try
            {
                if (courses is null)
                {
                    throw new CourseRepositoryException("Could not get list of courses.");
                }

                return courses.AsEnumerable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Try using AsNoTracking and AsAsyncEnumerable, and a select statement, only extracting what we need for the course overview response?
        public async Task<IEnumerable<Course>> GetCreatedCourses(int profileId)
        {
            try
            {
                var courses = await _context.Courses.Where(c => c.Owner.Id == profileId).ToListAsync();

                return courses;
            }
            catch (Exception)
            { 
                throw new CourseRepositoryException($"Could not get created courses for user: {profileId}.");
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
