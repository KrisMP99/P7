using Microsoft.EntityFrameworkCore;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; }
        DbSet<ExerciseGroup> ExerciseGroups { get; }
        public DbSet<Exercise> Exercises { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
