using Microsoft.EntityFrameworkCore;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;
using P7WebApp.Domain.Repositories;

namespace P7WebApp.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {

        private readonly IApplicationDbContext _context;

        public ExerciseRepository(IApplicationDbContext context)
        {
            _context = context;
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
                    throw new Exception();
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
                    throw new NullReferenceException();
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
                    throw new Exception();
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
                    throw new Exception();
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
                    throw new NullReferenceException();
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
                    throw new NullReferenceException();
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
