using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Exceptions;

namespace P7WebApp.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<ExerciseRepository> _logger;

        public ExerciseRepository(IApplicationDbContext context, ILogger<ExerciseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateExercise(Exercise exercise)
        {
            try
            {
                var result = await _context.Exercises.AddAsync(exercise);
                _logger.LogInformation("create exercise in repository");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Create exercise group failed with message: {ex.Message}");
                throw new ExerciseGroupRepositoryException($"Could not create exercise with Id: {exercise.Id}, due to: {ex.Message}");
            }
        }
        public async Task<Exercise> GetExerciseWithSolutionsById(int id)
        {
            try
            {
                var exercise = await _context.Exercises.Include(e => e.Solutions).FirstOrDefaultAsync();
                if (exercise is not null)
                {
                    return exercise;
                }
                else
                {
                    throw new ExerciseRepositoryException($"Could not get exercise with solution for exercise with Id: {id}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Exercise> GetExerciseWithSubmissionsById(int id)
        {
            try
            {
                var exercise = await _context.Exercises.Include(e => e.Submissions).FirstOrDefaultAsync();
                if (exercise is not null) 
                { 
                    return exercise;
                }
                else
                {
                    throw new ExerciseRepositoryException($"Could not get exercise with submission for exercise with Id: {id}.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Exercise> GetExerciseWithModulesById(int id)
        {
            try
            {
                var exercise = await _context.Exercises.Where(e => e.Id == id).Include(e => e.Modules).FirstOrDefaultAsync();
                if (exercise is not null)
                {
                    return exercise;
                }
                else
                {
                    throw new ExerciseRepositoryException($"Could not get exercise with modules for exercise with Id: {id}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateModule(Module module)
        {
            return 1;
        }

        public Task<int> CreateSolution(Solution solution)
        {
            throw new NotImplementedException();
        }

        public Task<int> Createsubmission(Submission submission)
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateExercise(Exercise exercise)
        {
            try
            {
                var courseUpdate = _context.Exercises.Update(exercise);

                if (courseUpdate != null)
                {
                    return 1;
                }
                else
                {
                    throw new ExerciseRepositoryException($"Could not update exercise with Id: {exercise.Id}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteModule(Module module)
        {
            return 1;
        }

        public async Task<int> DeleteSolution(Solution solution)
        {
            try
            {
                var exercise = await GetExerciseWithSolutionsById(solution.ExerciseId);
                if (exercise is not null)
                {
                    exercise.RemoveSolutionById(solution.Id);
                    return 1;
                }
                else
                {
                    throw new NullReferenceException($"Could not delete solution with Id: {solution.Id}.");
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> DeleteSubmission(Submission submission)
        {
            try
            {
                var exercise = await GetExerciseWithSubmissionsById(submission.ExerciseId);
                if (exercise is not null)
                {
                    exercise.RemoveSubmissionById(submission.Id);
                    return 1;
                }
                else
                {
                    throw new NullReferenceException($"Could not delete solution with Id: {submission.Id}.");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Exercise> GetExerciseFromModuleId(int moduleId)
        {
            throw new NotImplementedException();
        }

        public async Task<Exercise> GetExerciseFromSolutionId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Exercise> GetExerciseFromSubmissionId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Exercise> GetExerciseWithModules(int exerciseGroupId, int exerciseId)
        {
            try
            {
                var exercise = await _context.Exercises.Where(e => e.ExerciseGroupId == exerciseGroupId && e.Id == exerciseId)
                    .Include(e => e.Modules)
                        .ThenInclude(m => (m as TextModule).Images)
                    .Include(e => e.Modules)
                        .ThenInclude(m => (m as QuizModule).Questions)
                        .ThenInclude(qm => qm.Choices)
                    .FirstOrDefaultAsync();

                return exercise;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
