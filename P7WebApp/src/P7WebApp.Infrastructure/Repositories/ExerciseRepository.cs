using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {

        private readonly IApplicationDbContext _context;

        public ExerciseRepository(IApplicationDbContext context)
        {
            _context = context;
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

        public async Task<int> DeleteModule(Module module)
        {
            return 1;
        }

        public Task<int> DeleteSolution(Solution solution)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSubmission(Submission submission)
        {
            throw new NotImplementedException();
        }

        public async Task<Exercise> GetExerciseById(int id)
        {
            return new Exercise() { Id = id };
        }

        public async Task<int> UpdateExercise(Exercise exercise)
        {
            return 1;
        }
    }
}
