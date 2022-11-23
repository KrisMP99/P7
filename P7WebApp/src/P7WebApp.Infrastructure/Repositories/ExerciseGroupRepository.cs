using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Common;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Models;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
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

        public async Task CreateExercise(Exercise exercise)
        {
            try
            {
                var result = await  _context.Exercises.AddAsync(exercise);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteExercise(int exerciseId)
        {
            try
            {
                var exercise = _context.Exercises.Find(exerciseId);
                if (exercise != null)
                {
                    _context.Exercises.Remove(exercise);
                    return exerciseId;
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

        public async Task<ExerciseGroup> GetExerciseGroupByIdWithExercises(int Id)
        {
            try
            {
                var exerciseGroup = await _context.ExerciseGroups.Include(e => e.Exercises).FirstOrDefaultAsync();
                
                if (exerciseGroup is not null)
                {
                    return exerciseGroup;
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

        public async Task<IAsyncEnumerable<ExerciseGroup>> GetExerciseGroupsByCourseId(int courseId)
        {
            try
            {
                var courses = await _context.Courses.FindAsync(courseId);
                
                if(courses is not null)
                {
                    return (IAsyncEnumerable<ExerciseGroup>)courses.ExerciseGroups;
                }

                return null;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Task<int> UpdateExerciseGroup(ExerciseGroup course)
        {
            throw new NotImplementedException();
        }
    }
}
