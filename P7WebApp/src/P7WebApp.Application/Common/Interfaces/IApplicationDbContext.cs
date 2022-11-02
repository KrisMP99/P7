using Microsoft.EntityFrameworkCore;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseGroupAggregateRoot;

namespace P7WebApp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; }
        DbSet<ExerciseGroup> ExerciseGroups { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
