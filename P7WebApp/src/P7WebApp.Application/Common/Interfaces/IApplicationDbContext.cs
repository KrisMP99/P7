using Microsoft.EntityFrameworkCore;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Aggregates.ProfileAggregate;

namespace P7WebApp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Course> Courses { get; }
        public DbSet<Attendee> Attendees { get; }
        public DbSet<ExerciseGroup> ExerciseGroups { get; }
        public DbSet<Exercise> Exercises { get; }
        public DbSet<CourseRole> CourseRoles { get; }
        public DbSet<Permission> Permissions { get; }
        public DbSet<Submission> Submissions { get; }
        public DbSet<Solution> Solutions { get; }
        public DbSet<TextModule> TextModules { get; }
        public DbSet<CodeEditorModule> CodeEditorModules { get; }
        public DbSet<QuizModule> QuizModules { get; }
        public DbSet<TestCase> TestCases { get; }
        public DbSet<Choice> Choices { get; }
        public DbSet<Module> Modules { get; }
        public DbSet<Profile> Profiles { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
