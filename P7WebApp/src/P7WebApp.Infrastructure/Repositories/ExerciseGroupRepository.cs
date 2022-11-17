﻿using P7WebApp.Application.Common.Interfaces;
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

        public Task<int> CreateExercise(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteExercise(int exerciseId)
        {
            throw new NotImplementedException();
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
