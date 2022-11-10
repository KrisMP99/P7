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
using P7WebApp.Infrastructure.Common;
using P7WebApp.Infrastructure.Identity;
using P7WebApp.Infrastructure.Persistence.Intercepters;

namespace P7WebApp.Infrastructure.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        private readonly IAuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions, 
            IMediator mediator,
            IAuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options, operationalStoreOptions)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<ExerciseGroup> ExerciseGroups { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<CourseRole> CourseRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<SubmissionDraft> SubmissionsDrafts { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<TextModule> TextModules { get; set; }
        public DbSet<CodeEditorModule> CodeEditorModules { get; set; }
        public DbSet<QuizModule> QuizModules { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Module> Modules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Module>().UseTpcMappingStrategy();
            base.OnModelCreating(builder);
        }
    }
}