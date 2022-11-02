using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.AggregateRoots.ExerciseGroupAggregateRoot;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Infrastructure.Repositories
{
    public class ExerciseGroupRepository : IExerciseGroupRepository
    {
        private readonly IApplicationDbContext _context;

        public ExerciseGroupRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CreateExerciseGroup(ExerciseGroup course)
        {
            throw new NotImplementedException();
        }

        public Task<ExerciseGroup> GetExerciseGroupByCourseId(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<ExerciseGroup> GetExerciseGroupByGroupId(int exerciseGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExerciseGroup>> GetExerciseGroupsByCourseId(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateExerciseGroup(ExerciseGroup course)
        {
            throw new NotImplementedException();
        }
    }
}
