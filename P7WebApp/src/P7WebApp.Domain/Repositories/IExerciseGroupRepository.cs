﻿using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Domain.Repositories
{
    public interface IExerciseGroupRepository
    {
        Task<ExerciseGroup> GetExerciseGroupByGroupId(int exerciseGroupId);
        Task<IAsyncEnumerable<ExerciseGroup>> GetExerciseGroupsByCourseId(int courseId);
        Task<int> UpdateExerciseGroup(ExerciseGroup course);
        Task<int> CreateExercise(Exercise exercise);
        Task<int> DeleteExercise(int exerciseId);
    }
}
