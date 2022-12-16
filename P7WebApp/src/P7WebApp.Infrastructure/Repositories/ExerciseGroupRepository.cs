using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Services.Common;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Models;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Exceptions;

namespace P7WebApp.Infrastructure.Repositories
{
    public class ExerciseGroupRepository : IExerciseGroupRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<ExerciseGroupRepository> _logger;

        public ExerciseGroupRepository(IApplicationDbContext context, ILogger<ExerciseGroupRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateExerciseGroupAsync(ExerciseGroup exerciseGroup)
        {
            try
            {
                var result = await _context.ExerciseGroups.AddAsync(exerciseGroup);
                _logger.LogInformation("create exercise group in repository");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Create exercise group failed with message: {ex.Message}");
                throw new ExerciseGroupRepositoryException($"Could not create exercise with Id: {exerciseGroup.Id}, due to: {ex.Message}");
            }
        }

        public async Task CreateExercise(Exercise exercise)
        {
            try
            {
                var result = await  _context.Exercises.AddAsync(exercise);
                _logger.LogInformation("create exercise in repository");
            }
            catch (Exception ex)
            {
                throw new ExerciseGroupRepositoryException($"Could not create exercise with Id: {exercise.Id}, due to: {ex.Message}");
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
                    throw new ExerciseGroupRepositoryException($"Could not delete exercise with Id: {exerciseId}.");
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
                var exerciseGroup = await _context.ExerciseGroups.Where(eg => eg.Id == Id).Include(eg => eg.Exercises).FirstOrDefaultAsync();
                
                if (exerciseGroup is not null)
                {
                    return exerciseGroup;
                }
                else
                {
                    throw new ExerciseGroupRepositoryException($"Could not get exercise group with exercises with Id: {exerciseGroup.Id}.");
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
                throw new ExerciseGroupRepositoryException($"Could not get exercise group for course with Id: {courseId}.");
            }
        }

        public async Task<int> UpdateExerciseGroup(ExerciseGroup exerciseGroup)
        {
            try
            {
                var courseUpdate = _context.ExerciseGroups.Update(exerciseGroup);

                if (courseUpdate != null)
                {
                    return 1;
                }
                else
                {
                    throw new ExerciseRepositoryException($"Could not update exercise group with Id: {exerciseGroup.Id}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
