using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
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

        public async Task<Exercise> GetExerciseById(int id)
        {
            return new Exercise(1, "", false, 1, null, null, null, null, 1);
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
            return 1;
        }

        public async Task<int> DeleteSubmission(Submission submission)
        {
            return 1;
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
    }
}
