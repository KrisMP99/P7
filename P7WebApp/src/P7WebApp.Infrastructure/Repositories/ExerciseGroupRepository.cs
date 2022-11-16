using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Infrastructure.Repositories
{
    public class ExerciseGroupRepository : IExerciseGroupRepository
    {
        private readonly IApplicationDbContext _context;

        public ExerciseGroupRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateExerciseGroup(ExerciseGroup exerciseGroup)
        {
            try
            {
                await _context.ExerciseGroups.AddAsync(exerciseGroup);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ExerciseGroup> GetExerciseGroupByGroupId(int exerciseGroupId)
        {
            try
            {
                var exerciseGroup = await _context.ExerciseGroups.FindAsync(exerciseGroupId);
                return exerciseGroup;
            }
            catch(Exception)
            {
                throw;
            }

        }

        public async Task<IEnumerable<ExerciseGroup>> GetExerciseGroupsByCourseId(int courseId)
        {
            try
            {
                var course = await _context.Courses.FindAsync(courseId);
                
                if (course is not null)
                {
                    return course.ExerciseGroups;
                }

                return null;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
