using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Aggregates.ProfileAggregate;
using P7WebApp.Infrastructure.Common;
using P7WebApp.Infrastructure.Identity;
using System.Reflection.Emit;

namespace P7WebApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            IMediator mediator
            ) : base(options, operationalStoreOptions)
        {
            _mediator = mediator;
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<ExerciseGroup> ExerciseGroups { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<CourseRole> CourseRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<TextModule> TextModules { get; set; }
        public DbSet<CodeEditorModule> CodeEditorModules { get; set; }
        public DbSet<QuizModule> QuizModules { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Module>()
            .UseTpcMappingStrategy();

            builder.Entity<Exercise>()
                .HasMany(e => e.Modules)
                .WithOne(m => m.Exercise)
                .HasForeignKey(m => m.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Submission>()
                .HasMany(e => e.Modules)
                .WithOne(m => m.Submission)
                .HasForeignKey(m => m.SubmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Solution>()
                .HasMany(e => e.Modules)
                .WithOne(m => m.Solution)
                .HasForeignKey(m => m.SolutionId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}