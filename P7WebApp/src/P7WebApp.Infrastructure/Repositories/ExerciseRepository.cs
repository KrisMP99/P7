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

        public Task<Module> AddModule(int moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<Solution> AddSolution(Solution solution)
        {
            throw new NotImplementedException();
        }

        public Task<Submission> AddSubmission(Submission submission)
        {
            throw new NotImplementedException();
        }

        public Task<Module> DeleteModule(Module module)
        {
            throw new NotImplementedException();
        }

        public Task<Solution> DeleteSolution(Solution solution)
        {
            throw new NotImplementedException();
        }

        public Task<Submission> DeleteSubmission(Submission submission)
        {
            throw new NotImplementedException();
        }

        public Task<Exercise> UpdateInformation(Exercise exercise)
        {
            throw new NotImplementedException();
        }
    }
}
